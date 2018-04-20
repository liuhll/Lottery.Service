using ECommon.Components;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Lottery.Crawler;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.TimeRule;
using Lottery.QueryServices.Lotteries;
using Lottery.RunApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lottery.RunApp.Jobs
{
    public abstract class RunLotteryAbstractJob : ILotteryJob
    {
        protected string _lotteryCode;
        protected readonly ILotteryQueryService _lotteryQueryService;
        protected readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;

        protected ITimeRuleManager _timeRuleManager;
        protected LotteryFinalDataDto _lotteryFinalData;
        protected LotteryInfoDto _lotteryInfo;
        protected ICommandService _commandService;
        protected ILotteryDataQueryService _lotteryDataQueryService;

        protected const int LotteryDataDelay = 200;

        protected IList<IDataUpdateItem> _dataUpdateItems;

        protected static bool _isCrawling = false;
        protected static object _objLock = new object();

        protected static bool _isStart;

        public DateTime LastStart { get; }
        public DateTime LastEnd { get; }
        public bool StopOnError { get; }

        public event EventHandler<LotteryJobEventArgs> EachTaskExcuteAfterHandler;

        protected RunLotteryAbstractJob()
        {
            PreInitialize();
            LastStart = DateTime.Now;
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _lotteryFinalDataQueryService = ObjectContainer.Resolve<ILotteryFinalDataQueryService>();
            _commandService = ObjectContainer.Resolve<ICommandService>();
            _lotteryDataQueryService = ObjectContainer.Resolve<ILotteryDataQueryService>();
            _lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(_lotteryCode);
            _timeRuleManager = new TimeRuleManager(_lotteryInfo);
            _lotteryFinalData = _lotteryFinalDataQueryService.GetFinalData(_lotteryInfo.Id);
            _dataUpdateItems = DataUpdateContext.GetDataUpdateItems(_lotteryInfo.Id);
            _isStart = true;
            PostinItialize();
        }

        protected LotteryFinalDataDto LotteryFinalData => _lotteryFinalData;

        protected abstract void PreInitialize();

        protected abstract void PostinItialize();

        /// <summary>
        /// 执行定时任务
        /// </summary>
        public virtual void Execute()
        {
            // 处于开奖期间,或是最后一期开奖
            if (_timeRuleManager.IsLotteryDuration || _timeRuleManager.IsTodayFinalPeriod)
            {
                DateTime nextDateTime;

                if (_timeRuleManager.ParseNextLotteryTime(out nextDateTime))
                {
                    if (!JudgeCurrentPeriodIsLottery() || _isStart)
                    {
                        IList<LotteryDataDto> lotteryDatas = null;
                        if (!_isCrawling)
                        {
                            _isCrawling = true;
                            lock (_objLock)
                            {
                                // 抓取新的数据
                                foreach (var updateItem in _dataUpdateItems)
                                {
                                    var crawlNewDatas = updateItem.CrawlDatas(LotteryFinalData.FinalPeriod);
                                    if (crawlNewDatas.Safe().Any())
                                    {
                                        lotteryDatas = crawlNewDatas.Safe().OrderBy(p => p.Period).ToList();
                                        break;
                                    }
                                }

                                if (lotteryDatas.Safe().Any())
                                {
                                    var crawlFinalData = lotteryDatas.Last();

                                    VerifyTodayFirstPeriod(crawlFinalData);

                                    foreach (var lotteryData in lotteryDatas)
                                    {
                                        if (lotteryData.Period > LotteryFinalData.FinalPeriod)
                                        {
                                            var thisPeriodLotteryData =
                                                _lotteryDataQueryService.GetPredictPeriodData(_lotteryInfo.Id,
                                                    lotteryData.Period);
                                            if (thisPeriodLotteryData == null)
                                            {
                                                CommandExecute(new AddLotteryDataCommand(Guid.NewGuid().ToString(), lotteryData));
                                                Thread.Sleep(LotteryDataDelay);
                                                _lotteryFinalData = _lotteryFinalDataQueryService.GetFinalData(lotteryData.LotteryId);
                                            }
                                        }
                                    }
                                    if (_timeRuleManager.IsTodayFinalPeriod)
                                    {
                                        var todayLastLotteryData =
                                            lotteryDatas.OrderByDescending(p => p.Period).First();
                                        UpdateNextFirstPeriod(todayLastLotteryData);
                                    }

                                    EachTaskExcuteAfterHandler?.Invoke(this, new LotteryJobEventArgs(_lotteryCode, _lotteryInfo.Id, _lotteryFinalData));
                                    //int dayFirstPeriod = 0;
                                    //if (IsNeedSetFirstPeriod(out dayFirstPeriod))
                                    //{
                                    //    SendCommandAsync(new UpdateNextDayFirstPeriodCommand(LotteryFinalData.Id, LotteryFinalData.LotteryId, dayFirstPeriod));
                                    //}
                                }
                            }

                            _isCrawling = false;
                            if (_isStart)
                            {
                                _isStart = false;
                            }
                        }
                    }
                }
            }
        }

        private void VerifyTodayFirstPeriod(LotteryDataDto crawlFinalData)
        {
            var startTimePoint = _timeRuleManager.TodayTimeRule.StartTime.TotalSeconds;
            var endTimePoint = crawlFinalData.LotteryTime.TimeOfDay.TotalSeconds;
            var interval = _timeRuleManager.TodayTimeRule.Tick.TotalSeconds;

            var currentCount = Convert.ToInt32(Math.Ceiling((endTimePoint - startTimePoint) / interval));
            var computeTodayFirstPeriod = crawlFinalData.Period - currentCount + 1;

            if (computeTodayFirstPeriod != _lotteryFinalData.TodayFirstPeriod)
            {
                SendCommandAsync(new UpdateNextDayFirstPeriodCommand(LotteryFinalData.Id, crawlFinalData.LotteryId, computeTodayFirstPeriod));
            }
        }

        private void UpdateNextFirstPeriod(LotteryDataDto crawlFinalData)
        {
            var nextDayFirstPeriod = crawlFinalData.Period + 1;
            SendCommandAsync(new UpdateNextDayFirstPeriodCommand(LotteryFinalData.Id, crawlFinalData.LotteryId, nextDayFirstPeriod));
        }

        protected int TodayActualLotteryCount => LotteryFinalData.FinalPeriod - LotteryFinalData.TodayFirstPeriod + 1;

        /// <summary>
        /// 判断当前期是否已经开奖
        /// </summary>
        /// <returns></returns>
        private bool JudgeCurrentPeriodIsLottery()
        {
            //当前实际上的开奖期数
            //  var actualLotteryCount = LotteryFinalData.FinalPeriod - LotteryFinalData.TodayFirstPeriod + 1;

            // 今日开到的期数
            var todayCurrentCount = _timeRuleManager.TodayCurrentCount;

            if (todayCurrentCount == -1)
            {
                // 当天没有预设的开奖规则,不用开奖
                return true;
            }
            // 当日还未开奖
            if (LotteryFinalData.FinalPeriod < LotteryFinalData.TodayFirstPeriod)
            {
                return false;
            }

            // 超过一天未开奖，服务器意外停止
            if (TodayActualLotteryCount >= _timeRuleManager.TodayTotalCount)
            {
                return false;
            }

            // 当前期数还未开奖
            if (TodayActualLotteryCount < todayCurrentCount)
            {
                return false;
            }

            return true;
        }

        private bool IsNeedSetFirstPeriod(out int todayFirstPeriod)
        {
            if (_timeRuleManager.IsTodayFinalPeriod)
            {
                todayFirstPeriod = LotteryFinalData.FinalPeriod + 1;
                return true;
            }
            //else if(_timeRuleManager.TodayCurrentCount > 0)
            //{
            //    todayFirstPeriod = LotteryFinalData.FinalPeriod - _timeRuleManager.TodayCurrentCount + 1;
            //    if (todayFirstPeriod != LotteryFinalData.TodayFirstPeriod)
            //    {
            //        return true;
            //    }
            //}
            todayFirstPeriod = LotteryFinalData.TodayFirstPeriod;
            return false;
        }

        /// <summary>异步发送给定的命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        private Task<AsyncTaskResult> SendCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.SendAsync(command).TimeoutAfter(millisecondsDelay);
        }

        /// <summary> 执行命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        private void CommandExecute(ICommand command, int millisecondsDelay = 5000)
        {
            _commandService.Execute(command, millisecondsDelay);
        }
    }
}