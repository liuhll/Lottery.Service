using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Lottery.Crawler;
using Lottery.Dtos.Lotteries;
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
        protected ICommandService _commandService;

        protected IList<IDataUpdateItem> _dataUpdateItems;

        protected bool _isCrawling = false;

      
        protected RunLotteryAbstractJob()
        {
            PreInitialize();

            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _lotteryFinalDataQueryService = ObjectContainer.Resolve<ILotteryFinalDataQueryService>();
            _commandService = ObjectContainer.Resolve<ICommandService>();

            _lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(_lotteryCode);
            _timeRuleManager = new TimeRuleManager(_lotteryInfo);           
            _lotteryFinalData = _lotteryFinalDataQueryService.GetFinalData(_lotteryInfo.Id);
            _dataUpdateItems = DataUpdateContext.GetDataUpdateItems(_lotteryInfo.Id);

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
                        IList<LotteryDataDto> lotteryDatas = null;
                        if (!_isCrawling)
                        {
                            _isCrawling = true;                            
                            // 抓取新的数据
                            _dataUpdateItems.ForEach(updateItem =>
                            {
                               
                                var crawlNewDatas = updateItem.CrawlDatas(_lotteryFinalData.FinalPeriod);
                                if (crawlNewDatas.Safe().Any())
                                {
                                    lotteryDatas = crawlNewDatas;
                                }
                                
                            });

                            if (lotteryDatas.Safe().Any())
                            {
                                var result = _commandService.ExecuteAsync(
                                    new NewLotteryCommand(Guid.NewGuid().ToString(), lotteryDatas.First().Period,
                                        lotteryDatas.First().LotteryId, lotteryDatas.First().Data,
                                        lotteryDatas.First().LotteryTime)).Result;
                            }

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