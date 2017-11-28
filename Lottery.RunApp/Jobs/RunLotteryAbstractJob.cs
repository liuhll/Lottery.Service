using System.Collections.Generic;
using ECommon.Components;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.TimeRules;
using Lottery.Engine.TimeRule;
using Lottery.QueryServices.Lotteries;

namespace Lottery.RunApp.Jobs
{
    public abstract class RunLotteryAbstractJob : AbstractJob
    {
        protected string _lotteryCode;
        protected readonly ILotteryQueryService _lotteryQueryService;
        protected LotteryInfoDto _lotteryInfo;
        protected ITimeRuleManager _timeRuleManager;

        protected RunLotteryAbstractJob()
        {
            PreinItialize();

            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(_lotteryCode);
            _timeRuleManager = new TimeRuleManager(_lotteryInfo);
        }

        protected abstract void PreinItialize();

    }
}