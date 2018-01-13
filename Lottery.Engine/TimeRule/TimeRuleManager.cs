using System;
using System.Collections.Generic;
using System.Linq;
using DateTimeExtensions;
using DateTimeExtensions.TimeOfDay;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Lotteries;
using Expr = MathNet.Symbolics.Expression;

namespace Lottery.Engine.TimeRule
{
    [Component]
    public class TimeRuleManager : ITimeRuleManager
    {

        private readonly ITimeRuleQueryService _timeRuleQueryService;


        private LotteryInfoDto _lotteryInfo;

        private ICollection<TimeRuleDto> _timeRules;

        private LotteryFinalDataDto _lotteryFinalData;

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

        public DateTime? NextLotteryTime()
        {
            if (IsLotteryDuration)
            {
                var todayStartTime = DateTime.Now.StartTime();

                var nextTimeInterval = TodayTimeRule.Tick.TotalSeconds * TodayCurrentCount;

                return todayStartTime.Add(TodayTimeRule.StartTime).AddSeconds(nextTimeInterval);

            }
            else
            {
                if (TodayTimeRule != null)
                {
                    if (DateTime.Now.TimeOfDay.TotalSeconds > TodayTimeRule.EndTime.TotalSeconds)
                    {
                        var todayStartTime1 = DateTime.Now.AddDays(1).StartTime();
                        return todayStartTime1.Add(TodayTimeRule.StartTime);
                    }
                    var todayStartTime2 = DateTime.Now.StartTime();
                    return todayStartTime2.Add(TodayTimeRule.StartTime);
                }
                // Todo: 解析其他可能性
                return null;
            }

        }

        public bool ParseNextLotteryTime(out DateTime nextLotteryTime)
        {
            var lotteryTime = NextLotteryTime();
            if (lotteryTime == null)
            {
                nextLotteryTime = DateTime.MinValue;
                return false;
            }
           
            nextLotteryTime = lotteryTime.Value;
            return true;

        }

        public int TodayCurrentCount
        {
            get
            {
                if (!IsLotteryDuration)
                {
                    return 0;
                }

                var toadyTimeRule = TimeRules.FirstOrDefault(p => p.Weekday == (int)DateTime.Now.DayOfWeek);
                if (toadyTimeRule == null)
                {
                    return -1;
                }

                var startTimePoint = toadyTimeRule.StartTime.TotalSeconds;
                var endTimePoint = DateTime.Now.TimeOfDay.TotalSeconds;
                var interval = toadyTimeRule.Tick.TotalSeconds;

                return Convert.ToInt32(Math.Ceiling((endTimePoint - startTimePoint) / interval));
            }
        }


        public int TodayTotalCount
        {
            get
            {
                var toadyTimeRule = TimeRules.FirstOrDefault(p => p.Weekday == (int)DateTime.Now.DayOfWeek);
                if (toadyTimeRule == null)
                {
                    return 0;
                }

                var startTimePoint = toadyTimeRule.StartTime.TotalSeconds;
                var endTimePoint = toadyTimeRule.EndTime.TotalSeconds;
                var interval = toadyTimeRule.Tick.TotalSeconds;

                return Convert.ToInt32((endTimePoint - startTimePoint + interval) / interval);
            }
        }
        

        public bool IsLotteryDuration
        {
            get
            {
                if (TodayTimeRule == null)
                {
                    return false;
                }
                if (DateTime.Now.IsBetween(Time.Parse(TodayTimeRule.StartTime.ToString()), Time.Parse(TodayTimeRule.EndTime.ToString())))
                {
                    return true;
                }
                return false;
            }
        }

        public TimeRuleDto TodayTimeRule
        {
            get
            {
                var toadyTimeRule = TimeRules.FirstOrDefault(p => p.Weekday == (int)DateTime.Now.DayOfWeek);            
                return toadyTimeRule;
            }

        }

        public bool IsTodayFinalPeriod => TodayCurrentCount == TodayTotalCount;

        public bool FinalPeriodIsLottery(LotteryFinalDataDto finalData)
        {
            var todayActLotteryCount = finalData.FinalPeriod - finalData.TodayFirstPeriod + 1;
            // 当前期数还未开奖
            if (todayActLotteryCount < TodayCurrentCount)
            {
                return false;
            }
            return true;
        }

        private TimeRuleDto NextDayTimeRule(TimeRuleDto currentDay,int step = 1)
        {
            var nextWeekDay = currentDay.Weekday + step;
            if (nextWeekDay >= 7)
            {
                nextWeekDay = 0;
            }

            var nextDay = TimeRules.FirstOrDefault(p => p.Weekday == nextWeekDay);
            if (nextDay != null)
            {
                return nextDay;
            }
            return NextDayTimeRule(currentDay, step + 1);
        }

       
    }
}