using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.TimeRules;
using Lottery.Crawler;
using Lottery.Engine.TimeRule;
using Lottery.QueryServices.Lotteries;

namespace Lottery.RunApp.Jobs
{
    public abstract class RunLotteryAbstractJob : AbstractJob
    {
        protected string _lotteryCode;
        protected readonly ILotteryQueryService _lotteryQueryService;
        protected readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;
        protected ITimeRuleManager _timeRuleManager;
        
        protected LotteryInfoDto _lotteryInfo;
        protected LotteryFinalDataDto _lotteryFinalData;

        protected IList<IDataUpdateItem> _dataUpdateItems;

        protected bool _isCrawling = false;

      
        protected RunLotteryAbstractJob()
        {
            PreInitialize();

            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _lotteryFinalDataQueryService = ObjectContainer.Resolve<ILotteryFinalDataQueryService>();

            _lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(_lotteryCode);
            _timeRuleManager = new TimeRuleManager(_lotteryInfo);           
            _lotteryFinalData = _lotteryFinalDataQueryService.GetFinalData(_lotteryInfo.Id);

            PostinItialize();
        }

        
        protected abstract void PreInitialize();

        protected abstract void PostinItialize();

        public override void Execute()
        {
            // 处于开奖期间,或是最后一期开奖
            if (_timeRuleManager.IsLotteryDuration || !_timeRuleManager.IsFinalPeriod)
            {
                DateTime nextDateTime;

                if (_timeRuleManager.NextLotteryTime(out nextDateTime))
                {                 
                    if (!JudgeCurrentPeriodIsLottery())
                    {
                        if (!_isCrawling)
                        {
                            _isCrawling = true;
                            // 抓取新的数据
                            _dataUpdateItems.ForEach(updateItem =>
                            {
                               
                                var crawlNewDatas = updateItem.CrawlDatas(_lotteryFinalData.FinalPeriod);
                                if (crawlNewDatas.Safe().Any())
                                {
                                    
                                }
                                
                            });
                            _isCrawling = false;
                        }
                      

                    }
                }
            }
        }

        /// <summary>
        /// 判断当前期是否已经开奖
        /// </summary>
        /// <returns></returns>
        private bool JudgeCurrentPeriodIsLottery()
        {
            //当前实际上的开奖期数
            var actualLotteryCount = _lotteryFinalData.FinalPeriod - _lotteryFinalData.TodayFirstPeriod + 1;

            // 今日开到的期数
            var todayCurrentCount = _timeRuleManager.TodayCurrentCount;

            if (todayCurrentCount == -1)
            {
                // 当天没有预设的开奖规则,不用开奖
                return true;
            }
            // 当日还未开奖
            if (_lotteryFinalData.FinalPeriod < _lotteryFinalData.TodayFirstPeriod)
            {
                return false;
            }
            // 当前期数还未开奖
            if (actualLotteryCount < todayCurrentCount -1)
            {
                return false;
            }
       
            return true;
        }
    }
}