using System;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class RankJudgePerdictDataResult : BaseJudgePerdictDataResult
    {
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

        public override PredictedResult JudgePredictDataResult(LotteryInfoDto lotteryInfo, PredictDataDto startPeriodData,
            NormConfigDto userNormConfig)
        {
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            var lotteryData = _lotteryDataQueryService.GetPredictPeriodData(lotteryInfo.Id, startPeriodData.CurrentPredictPeriod);
            if (lotteryData == null)
            {
                return PredictedResult.Running;
            }
            var lotteryNumber = new LotteryNumber(lotteryData);
            var rank = planInfo.PositionInfos.First().Position;
            var lotteryNumberData = lotteryNumber.GetRankNumber(rank);
            if (startPeriodData.PredictedData.Contains(lotteryNumberData.ToString()))
            {
                if (planInfo.DsType == PredictType.Fix)
                {
                    return PredictedResult.Right;
                }

            }
            else
            {
                if (planInfo.DsType == PredictType.Kill)
                {
                    return PredictedResult.Right;
                }

            }
            if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
            {
                return PredictedResult.Error;
            }
            return PredictedResult.Running;

        }

        protected override object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo)
        {
            var lotteryData = String.Empty;
            var lotteryRank = lotteryNumber.Datas.IndexOf(postion);
            lotteryData = valList[lotteryRank];
            return lotteryData;
        }
    }
}