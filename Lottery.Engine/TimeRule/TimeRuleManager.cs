using System;
using System.Collections.Generic;
using System.Linq;
using DateTimeExtensions;
using DateTimeExtensions.TimeOfDay;
using ECommon.Components;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.TimeRule
{
    public class TimeRuleManager : ITimeRuleManager
    {

        private readonly ITimeRuleQueryService _timeRuleQueryService;

        private LotteryInfoDto _lotteryInfo;

        private ICollection<TimeRuleDto> _timeRules;

        public TimeRuleManager(LotteryInfoDto lotteryInfo)
        {
            _lotteryInfo = lotteryInfo;
            _timeRuleQueryService = ObjectContainer.Resolve<ITimeRuleQueryService>();

            InitTimeRule();
        }

        private void InitTimeRule()
        {
            _timeRules = _timeRuleQueryService.GetTimeRules(_lotteryInfo.Id);
        }

        public ICollection<TimeRuleDto> TimeRules => _timeRules;

        public DateTime NextLotteryTime()
        {
            throw new NotImplementedException();
        }

        public int TodayTotalCount {
            get
            {
                if (!IsLotteryDuration)
                {
                    return -1;
                }
                return 0;
            }
        }

        public int TodayCurrentCount { get; }

        public bool IsLotteryDuration
        {
            get
            {
                InitTimeRule();
                return  TodayTimeRule != null;
            }
        }

        public TimeRuleDto TodayTimeRule
        {
            get
            {
                var toadyTimeRule = TimeRules.FirstOrDefault(p => p.Weekday == (int) DateTime.Now.DayOfWeek);
                if (toadyTimeRule != null)
                {
                    if (DateTime.Now.IsBetween(Time.Parse(toadyTimeRule.StartTime.ToString()), Time.Parse(toadyTimeRule.EndTime.ToString())))
                    {
                        return toadyTimeRule;
                    }
                }
              
                return null;
            }

        }
    }
}