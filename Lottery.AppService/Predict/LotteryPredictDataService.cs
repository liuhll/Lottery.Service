using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using ECommon.Logging;
using Lottery.Dtos.Lotteries;
using Lottery.Engine;
using Lottery.Engine.ComputePredictResult;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Logs;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.Predict
{
    [Component]
    public class LotteryPredictDataService : ILotteryPredictDataService
    {
        private const string singleVal = "单";
        private const string doubleVal = "双";

        private const string longVal = "龙";
        private const string huVal = "虎";

        private const string bigVal = "大";
        private const string smallVal = "小";

        private string[] valList = new[]
        {
            "冠军",
            "亚军",
            "季军",
            "第四名",
            "第五名",
            "第六名",
            "第七名",
            "第八名",
            "第九名",
            "第十名",
        };

        private readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly ILotteryDataQueryService _lotteryDataQueryService;
        private readonly ILogger _logger;

        public LotteryPredictDataService(ILotteryFinalDataQueryService lotteryFinalDataQueryService,
            ILotteryQueryService lotteryQueryService,
            ILotteryPredictDataQueryService lotteryPredictDataQueryService,
            IPlanInfoQueryService planInfoQueryService,
            ILotteryDataQueryService lotteryDataQueryService)
        {
            _lotteryFinalDataQueryService = lotteryFinalDataQueryService;
            _lotteryQueryService = lotteryQueryService;
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
            _planInfoQueryService = planInfoQueryService;
            _lotteryDataQueryService = lotteryDataQueryService;
            _logger = NullLotteryLogger.Instance;
        }

        public ICollection<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm, int predictPeroid, string lotteryCode)
        {
            var predictDataResult = new Dictionary<int, PredictDataDto>();

            PredictDataDto lastPredictData;
            bool isNewPredict;

            var lastPredictPeriod = GetLastPredictNormPeriod(lotteryId, userNorm,lotteryCode,out lastPredictData,out isNewPredict);
            int startPredict;
            if (!isNewPredict)
            {
                startPredict = lastPredictData.StartPeriod;
                predictDataResult.Add(lastPredictData.StartPeriod, lastPredictData);
            }
            else
            {
                startPredict = lastPredictPeriod;
            }

            for (int i = lastPredictPeriod; i < predictPeroid; i++) // 新的预测期数是 startPredict = i + 1; 所以这里不能加 = 号
            {
                // 判断上一期的开奖情况
                if (NeedNewPredictData(i, startPredict, lotteryId, userNorm, ref predictDataResult, lotteryCode))
                {
                    startPredict = i + 1;
                    var thispredictData = PredictAppointedPeroidNormData(lotteryId, startPredict, userNorm);
                    predictDataResult.Add(startPredict, thispredictData);

                }

            }
            return predictDataResult.Values;

        }


        #region private Methods

        private bool NeedNewPredictData(int period, int startPredict, string lotteryId, NormConfigDto userNormConfig, ref Dictionary<int, PredictDataDto> predictDataResults, string lotteryCode)
        {
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            PredictDataDto startPeriodData = null;

            if (!predictDataResults.Safe().Any())
            {
                startPeriodData = _lotteryPredictDataQueryService.GetPredictDataByStartPeriod(startPredict, userNormConfig.Id, normPlanInfo.PlanNormTable, lotteryCode);
                if (startPeriodData == null)
                {
                    return true;
                }

                var predictedResult = JudgePredictDataResult(lotteryId, startPeriodData, userNormConfig);

                if (predictedResult == PredictedResult.Right)
                {
                    startPeriodData.EndPeriod = period;
                    startPeriodData.PredictedResult = (int)PredictedResult.Right;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return true;
                }
                else if (predictedResult == PredictedResult.Error)
                {
                    startPeriodData.EndPeriod = period;
                    startPeriodData.PredictedResult = (int)PredictedResult.Error;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return true;
                }
                else
                {
                    startPeriodData.CurrentPredictPeriod += 1;
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Running;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return false;
                }
            }
            else
            {
                startPeriodData = predictDataResults[startPredict];
                var predictedResult = JudgePredictDataResult(lotteryId, startPeriodData, userNormConfig);
                if (predictedResult == PredictedResult.Right)
                {

                    startPeriodData.EndPeriod = period;
                    startPeriodData.PredictedResult = (int)PredictedResult.Right;

                    return true;
                }
                else if (predictedResult == PredictedResult.Error)
                {
                    startPeriodData.EndPeriod = period;
                    startPeriodData.PredictedResult = (int)PredictedResult.Error;

                    return true;
                }
                else
                {
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.CurrentPredictPeriod += 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Running;

                    return false;
                }
            }
        }


        private PredictDataDto PredictAppointedPeroidNormData(string lotteryId, int predictPeriod, NormConfigDto userNorm)
        {
            var predictLotteryData = _lotteryDataQueryService.GetPredictPeriodDatas(lotteryId, predictPeriod - 1, userNorm.HistoryCount);

            var lotteryDataList = new LotteryDataList(predictLotteryData);
            var lotteryEngine = EngineContext.LotterEngine(lotteryId);
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);

            var predictData = GetPredictData(normPlanInfo, lotteryDataList);

            var positionInfo = normPlanInfo.PositionInfos.First();
            var count = positionInfo.MaxValue - positionInfo.MinValue + 1;
            string predictedData = String.Empty;
            IDictionary<int, double> predictedDataRate;
            try
            {
                AlgorithmType algorithmType = normPlanInfo.AlgorithmType;
                try
                {
                    predictedDataRate = lotteryEngine.GetPerdictor(algorithmType)
                        .Predictor(predictData, count, userNorm.UnitHistoryCount, userNorm.HistoryCount,new Tuple<int,int>(positionInfo.MinValue,positionInfo.MaxValue));
                }
                catch (Exception e)
                {
                    algorithmType = AlgorithmType.Mock;
                    predictedDataRate = lotteryEngine.GetPerdictor(algorithmType)
                        .Predictor(predictData, count, userNorm.UnitHistoryCount, userNorm.HistoryCount, new Tuple<int, int>(positionInfo.MinValue, positionInfo.MaxValue));
                }

                var computePredictResult = ComputePredictFatory.CreateComputePredictResult(normPlanInfo.PredictCode,predictedDataRate);

                predictedData = computePredictResult.GetPredictedData(normPlanInfo, userNorm);
                //predictedDataRate != null ? 
                //GetPredictedDataByRate(predictedDataRate, normPlanInfo.DsType, userNorm) 
                //: GetPredictedDataMock(normPlanInfo, userNorm);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                predictedData = GetPredictedDataMock(normPlanInfo, userNorm);

            }
            var predictDataInfo = new PredictDataDto()
            {
                NormConfigId = userNorm.Id,
                CurrentPredictPeriod = predictPeriod,
                StartPeriod = predictPeriod,
                EndPeriod = predictPeriod + userNorm.PlanCycle - 1,
                MinorCycle = 1,
                PredictedData = predictedData,
                PredictedResult = (int)PredictedResult.Running

            };

            return predictDataInfo;
        }

        private string GetPredictedDataMock(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var minVal = normPlanInfo.PositionInfos.Min(p => p.MinValue);
            var maxVal = normPlanInfo.PositionInfos.Min(p => p.MaxValue);
            var lotteryNumbers = new List<int>();
            for (int i = minVal; i <= maxVal; i++)
            {
                lotteryNumbers.Add(i);
            }
            lotteryNumbers = lotteryNumbers.OrderBy(p => Guid.NewGuid()).ToList();
            var result = lotteryNumbers.Take(userNorm.ForecastCount).ToString(",");
            return result;
        }

   
        private string GetPredictedDataByRate(IDictionary<int, double> predictedDataRate, PredictType dsType, NormConfigDto userNorm)
        {
            if (dsType == PredictType.Fix)
            {
                var predictedDataList = predictedDataRate.OrderByDescending(p => p.Value).Select(p => p.Key).ToList();
                var result = predictedDataList.Take(userNorm.ForecastCount).ToString(",");
                return result;
            }
            else
            {
                var predictedDataList = predictedDataRate.OrderBy(p => p.Value).Select(p => p.Key).ToList();
                var result = predictedDataList.Take(userNorm.ForecastCount).ToString(",");
                return result;
            }
        }

      
        private PredictedResult JudgePredictDataResult(string lotteryId,PredictDataDto startPeriodData, NormConfigDto userNormConfig)
        {
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            var lotteryData = _lotteryDataQueryService.GetPredictPeriodData(lotteryId, startPeriodData.CurrentPredictPeriod);
            if (lotteryData == null)
            {
                return PredictedResult.Running;
            }
            var lotteryNumber = new LotteryNumber(lotteryData);
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            if (planInfo.PlanPosition == PlanPosition.Single)
            {
                var postion = planInfo.PositionInfos.First().Position;
               
                var lotteryNumberData = GetLotteryNumberData(lotteryNumber, postion, normPlanInfo); //lotteryNumber[postion];
                bool isRight;
                if (normPlanInfo.PredictCode == PredictCodeDefinition.NumCode)
                {
                    var numPredictData = startPeriodData.PredictedData.Split(',').Select(p => Convert.ToInt32(p));
                    var numLotteryNum = Convert.ToInt32(lotteryNumberData);
                    isRight = numPredictData.Contains(numLotteryNum);

                }
                else
                {
                    isRight = startPeriodData.PredictedData
                        .Contains(lotteryNumberData.ToString());
                }
                if (isRight) //:todo bug
                {
                    if (planInfo.DsType == PredictType.Fix)
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
                else
                {
                    if (planInfo.DsType == PredictType.Kill)
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
            }
            else if (planInfo.PlanPosition == PlanPosition.Multiple)
            {
                var positions = planInfo.PositionInfos.Select(p => p.Position).ToArray();
                var lotteryNumbers = new List<int>(); // lotteryNumber.GetLotteryNumbers(positions);
                foreach (var position in positions)
                {
                    lotteryNumbers.Add(Convert.ToInt32(GetLotteryNumberData(lotteryNumber, position, normPlanInfo)));
                }
                var predictNumber = startPeriodData.PredictedData.Split(',').Select(p => Convert.ToInt32(p)).ToArray();
                if (planInfo.DsType == PredictType.Fix)
                {
                    if (lotteryNumbers.Any(p =>predictNumber.Contains(p)))
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
                else
                {
                    if (!lotteryNumbers.Any(p => predictNumber.Contains(p)))
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }

            }
            else
            {
                var rank = planInfo.PositionInfos.First().Position;
                var lotteryNumberData = lotteryNumber.GetRankNumber(rank);
                if (startPeriodData.PredictedData.Contains(lotteryNumberData.ToString()))
                {
                    if (planInfo.DsType == PredictType.Fix)
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
                else
                {
                    if (planInfo.DsType == PredictType.Kill)
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
            }
            
        }

        private object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo)
        {
            var numData = lotteryNumber[postion];
            string lotteryData = string.Empty;
            switch (planInfo.PredictCode)
            {
                case PredictCodeDefinition.NumCode:
                    lotteryData = lotteryNumber[postion].ToString();
                    break;
                case PredictCodeDefinition.LhCode:
                    var firstVal = lotteryNumber[postion];
                    var secondVal = lotteryNumber[lotteryNumber.Datas.Length - postion + 1];
                    if (firstVal > secondVal)
                    {
                        lotteryData = longVal;
                    }
                    else
                    {
                        lotteryData = huVal;
                    }
                    break;
                case PredictCodeDefinition.RankCode:
                    // lotteryData = valList[postion];
                    var lotteryRank = lotteryNumber.Datas.IndexOf(postion);
                    lotteryData = valList[lotteryRank];
                    break;
                case PredictCodeDefinition.ShapeCode:
                    if (numData % 2 == 0)
                    {
                        lotteryData = doubleVal;
                    }
                    else
                    {
                        lotteryData = singleVal;
                    }

                    break;
                case PredictCodeDefinition.SizeCode:
                    var sizeCriticalVal = planInfo.PositionInfos.First().MaxValue % 2;
                    if (numData > sizeCriticalVal)
                    {
                        lotteryData = bigVal;
                    }
                    else
                    {
                        lotteryData = smallVal;
                    }
                    break;  

            }
            return lotteryData;
        }


        private List<int> GetPredictData(PlanInfoDto normPlanInfo, LotteryDataList lotteryDataList)
        {
            var predictData = new List<int>();
            if (normPlanInfo.PlanPosition == PlanPosition.Single)
            {
                var positionInfo = normPlanInfo.PositionInfos.First();
                predictData.AddRange(lotteryDataList.LotteryDatas(positionInfo.Position));
            }
            else if (normPlanInfo.PlanPosition == PlanPosition.Multiple)
            {
                var positions = normPlanInfo.PositionInfos.Select(p => p.Position).ToArray();

                predictData.AddRange(lotteryDataList.LotteryDatas(NumberType.Number, positions));
            }
            else
            {
                var positionInfo = normPlanInfo.PositionInfos.First();
                predictData.AddRange(lotteryDataList.LotteryDatas(positionInfo.Position, NumberType.Rank));
            }
            return predictData;
        }


        private int GetLastPredictNormPeriod(string lotteryId, NormConfigDto userNorm, string lotteryCode,out PredictDataDto lastPredictData, out bool isNewPredict)
        {
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
            lastPredictData = _lotteryPredictDataQueryService.GetLastPredictData(userNorm.Id, normPlanInfo.PlanNormTable,lotteryCode);
            var lastLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictCount = userNorm.PlanCycle * userNorm.LookupPeriodCount;
            var theoryStartPredictPreoid = lastLotteryData.FinalPeriod - predictCount;
            if (lastPredictData != null)
            {
                var userNormPredictPeroid = lastPredictData.StartPeriod + lastPredictData.MinorCycle - 1;
                if (userNormPredictPeroid > theoryStartPredictPreoid)
                {
                    isNewPredict = false;
                    return userNormPredictPeroid;
                }
            }
            isNewPredict = true;
            return theoryStartPredictPreoid;
        }

        #endregion
    }
}