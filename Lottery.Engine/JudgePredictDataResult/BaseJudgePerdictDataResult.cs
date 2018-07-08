using System.Linq;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.JudgePredictDataResult
{
    public abstract class BaseJudgePerdictDataResult : IJudgePredictDataResult
    {
        protected readonly IPlanInfoQueryService _planInfoQueryService;
        protected readonly ILotteryDataQueryService _lotteryDataQueryService;

        protected BaseJudgePerdictDataResult()
        {
            _planInfoQueryService = ObjectContainer.Resolve<IPlanInfoQueryService>();
            _lotteryDataQueryService = ObjectContainer.Resolve<ILotteryDataQueryService>();
        }

        public abstract PredictedResult JudgePredictDataResult(LotteryInfoDto lotteryInfo,
            PredictDataDto startPeriodData,
            NormConfigDto userNormConfig);

        protected abstract object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo);
    }

}