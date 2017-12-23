using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.Dtos.Lotteries;
using Lottery.Engine;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.Predict
{
    [Component]
    public class LotteryPredictDataService : ILotteryPredictDataService
    {
        private readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly ILotteryDataQueryService _lotteryDataQueryService;

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
        }

        public ICollection<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm, int predictPeroid)
        {
            var predictDataResult = new Dictionary<int, PredictDataDto>();

            var lastPredictPeriod = GetLastPredictNormPeriod(lotteryId, userNorm);

            int startPredict = lastPredictPeriod;

            for (int i = lastPredictPeriod; i <= predictPeroid; i++)
            {
                // 判断上一期的开奖情况
                if (NeedNewPredictData(i, startPredict, lotteryId, userNorm,ref predictDataResult))
                {
                    startPredict = i;
                    var thispredictData = PredictAppointedPeroidNormData(lotteryId, lastPredictPeriod, userNorm);
                    predictDataResult.Add(startPredict, thispredictData);
                    
                }

            }

            return predictDataResult.Values;
        }




        #region private Methods

        private PredictDataDto PredictAppointedPeroidNormData(string lotteryId, int predictPeriod, NormConfigDto userNorm)
        {
            var predictLotteryData = _lotteryDataQueryService.GetPredictPeriodDatas(lotteryId, predictPeriod - 1, userNorm.HistoryCount);

            var lotteryDataList = new LotteryDataList(predictLotteryData);
            var lotteryEngine = EngineContext.LotterEngine(lotteryId);
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);

            var predictData = GetPredictData(normPlanInfo, lotteryDataList);

            var positionInfo = normPlanInfo.PositionInfos.First();
            var count = positionInfo.MaxValue - positionInfo.MinValue + 1;

            var predictedDataRate = lotteryEngine.GetPerdictor(normPlanInfo.PredictCode)
                .Predictor(predictData, count, userNorm.UnitHistoryCount);

            var predictedData = GetPredictedDataByRate(predictedDataRate, normPlanInfo.DsType,userNorm);


            var predictDataInfo = new PredictDataDto()
            {
                NormConfigId = userNorm.Id,
                CurrentPredictPeriod = predictPeriod,
                StartPeriod = predictPeriod,
                EndPeriod = predictPeriod + userNorm.PlanCycle,
                MinorCycle = 1,
                PredictedData = predictedData,
                PredictedResult = (int)PredictedResult.Running

            };

            return predictDataInfo;
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

        private bool NeedNewPredictData(int period, int startPredict,string lotteryId, NormConfigDto userNormConfig, ref Dictionary<int, PredictDataDto> predictDataResults)
        {
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);

            PredictDataDto startPeriodData = null;

            if (!predictDataResults.Safe().Any())
            {
                startPeriodData = _lotteryPredictDataQueryService.GetPredictDataByStartPeriod(startPredict, userNormConfig.Id, planInfo.PlanNormTable);
                if (startPeriodData == null)
                {
                    return true;
                }

                var predictedResult = JudgePredictDataResult(lotteryId,startPeriodData, period, userNormConfig);

                if (predictedResult == PredictedResult.Right)
                {

                    startPeriodData.EndPeriod = period;
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Right;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return true;
                }
                else if (predictedResult == PredictedResult.Error)
                {
                    startPeriodData.EndPeriod = period;
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Error;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return true;
                }
                else
                {
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Running;
                    predictDataResults.Add(startPredict, startPeriodData);
                    return false;
                }
            }
            else
            {
                startPeriodData = predictDataResults[startPredict];
                var predictedResult = JudgePredictDataResult(lotteryId,startPeriodData, period, userNormConfig);
                if (predictedResult == PredictedResult.Right)
                {

                    startPeriodData.EndPeriod = period;
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Right;
                    
                    return true;
                }
                else if (predictedResult == PredictedResult.Error)
                {
                    startPeriodData.EndPeriod = period;
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Error;
                  
                    return true;
                }
                else
                {
                    startPeriodData.MinorCycle = startPeriodData.MinorCycle + 1;
                    startPeriodData.PredictedResult = (int)PredictedResult.Running;
                    
                    return false;
                }
            }
        }

        private PredictedResult JudgePredictDataResult(string lotteryId,PredictDataDto startPeriodData, int period, NormConfigDto userNormConfig)
        {
            var lotteryEngine = EngineContext.LotterEngine(lotteryId);
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            var lotteryData = _lotteryDataQueryService.GetPredictPeriodData(lotteryId, period);
            var lotteryNumber = new LotteryNumber(lotteryData);

            if (planInfo.PlanPosition == PlanPosition.Single)
            {
                var postion = planInfo.PositionInfos.First().Position;
                var lotteryNumberData = lotteryNumber[postion];
                if (startPeriodData.PredictedData.Contains(lotteryNumberData.ToString()))
                {
                    if (planInfo.DsType == PredictType.Fix)
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
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
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
            }
            else if (planInfo.PlanPosition == PlanPosition.Multiple)
            {
                var positions = planInfo.PositionInfos.Select(p => p.Position).ToArray();
                var lotteryNumbers = lotteryNumber.GetLotteryNumbers(positions);

                var predictNumber = startPeriodData.PredictedData.Split(',').Select(p => Convert.ToInt32(p)).ToArray();

                if (planInfo.DsType == PredictType.Fix)
                {
                    if (lotteryNumbers.Any(p =>predictNumber.Contains(p)))
                    {
                        return PredictedResult.Right;
                    }
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
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
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
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
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
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
                    else if (startPeriodData.StartPeriod + userNormConfig.PlanCycle < period)
                    {
                        return PredictedResult.Error;
                    }
                    return PredictedResult.Running;
                }
            }
            
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


        private int GetLastPredictNormPeriod(string lotteryId, NormConfigDto userNorm)
        {
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
            var lastPredictData = _lotteryPredictDataQueryService.GetLastPredictData(userNorm.Id, normPlanInfo.PlanNormTable);
            var lastLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictCount = userNorm.PlanCycle * userNorm.LookupPeriodCount;
            var theoryStartPredictPreoid = lastLotteryData.FinalPeriod - predictCount;
            if (lastPredictData != null)
            {
                var userNormPredictPeroid = lastPredictData.StartPeriod + lastPredictData.MinorCycle - 1;
                if (userNormPredictPeroid > theoryStartPredictPreoid)
                {
                    return userNormPredictPeroid;
                }
            }

            return theoryStartPredictPreoid;
        }

        #endregion
    }
}