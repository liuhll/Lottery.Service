using ECommon.Components;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using FluentScheduler;
using Lottery.AppService.Predict;
using Lottery.Commands.LotteryPredicts;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.ScheduleTasks;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.ScheduleTasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.RunApp.Jobs
{
    public class JobFactory : Registry
    {
        private readonly IScheduleTaskQueryService _scheduleTaskQueryService;
        private readonly ICollection<ScheduleTaskDto> _scheduleTasks;
        private readonly ILotteryPredictDataService _lotteryPredictDataService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly ICommandService _commandService;

        private const string SystemUser = "System";

        public JobFactory()
        {
            _scheduleTaskQueryService = ObjectContainer.Resolve<IScheduleTaskQueryService>();
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _normConfigQueryService = ObjectContainer.Resolve<INormConfigQueryService>();
            _lotteryPredictDataService = ObjectContainer.Resolve<ILotteryPredictDataService>();
            _lotteryPredictDataQueryService = ObjectContainer.Resolve<ILotteryPredictDataQueryService>();
            _commandService = ObjectContainer.Resolve<ICommandService>();
            _scheduleTasks = _scheduleTaskQueryService.GetAllScheduleTaskInfos();
            InitScheduleJob();
        }

        private void InitScheduleJob()
        {
            Debug.Assert(_scheduleTasks != null && _scheduleTasks.Any(), "系统还未设置作业");

            foreach (var task in _scheduleTasks)
            {
                if (task.Enabled)
                {
                    var job = Activator.CreateInstance(Type.GetType(task.Type)) as IJob;
                    Schedule(job)
                        .ToRunNow()
                        .AndEvery(task.Seconds)
                        .Seconds();
                    if (job is ILotteryJob)
                    {
                        (job as ILotteryJob).EachTaskExcuteAfterHandler += JobNEachTaskExcuteAfterHandler;
                    }
                }
            }
        }

        private void JobNEachTaskExcuteAfterHandler(object sender, Events.LotteryJobEventArgs e)
        {
            var lotteryInfo = _lotteryQueryService.GetLotteryInfoById(e.LotteryId);
            var finalLotteryData = e.LotteryFinalData;
            var predictPeroid = finalLotteryData.FinalPeriod + 1;

            var predictDatas = new List<PredictDataDto>();
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryInfo.Id);
            foreach (var userNorm in userNorms)
            {
                predictDatas.AddRange(_lotteryPredictDataService.PredictNormData(lotteryInfo.Id, userNorm, predictPeroid, e.LotteryCode));
            }
            predictDatas.GroupBy(p => p.NormConfigId).ForEach(item =>
            {
                var planInfo = _normConfigQueryService.GetNormPlanInfoByNormId(item.Key, e.LotteryId);
                var newestPredictDataDto = item.OrderByDescending(p => p.StartPeriod).First();
                var normConfig = _normConfigQueryService.GetUserNormConfig(item.Key);
                var planTrackNumber = new PlanTrackNumber()
                {
                    NormId = normConfig.Id,
                    PlanId = planInfo.Id,
                    PlanName = planInfo.PlanName,
                    EndPeriod = newestPredictDataDto.EndPeriod,
                    StartPeriod = newestPredictDataDto.StartPeriod,
                    MinorCycle = newestPredictDataDto.MinorCycle,
                    PredictData = newestPredictDataDto.PredictedData,
                    CurrentPredictPeriod = newestPredictDataDto.CurrentPredictPeriod,
                    PredictType = planInfo.DsType,
                    HistoryPredictResults = GetHistoryPredictResults(item.OrderByDescending(p => p.StartPeriod), item.Key, normConfig.LookupPeriodCount, planInfo.PlanNormTable, lotteryInfo.LotteryCode),
                };
                var rightCount = planTrackNumber.HistoryPredictResults.Count(p => p == 0);
                var totleCount = planTrackNumber.HistoryPredictResults.Count(p => p != 2);
                var currentScore = Math.Round((double)rightCount / totleCount, 2);
                planTrackNumber.CurrentScore = currentScore;
                WritePlanTrackNumbers(item, planInfo, currentScore, lotteryInfo.LotteryCode);
            });

            Console.WriteLine(e.LotteryCode + ":" + e.LotteryFinalData.FinalPeriod + "-" + e.LotteryFinalData.Data);
        }

        #region private methods

        private int[] GetHistoryPredictResults(IOrderedEnumerable<PredictDataDto> predictDatas, string normId, int lookupPeriodCount, string planNormTable, string lotteryCode)
        {
            var historyPredictResults = new List<int>();
            ICollection<PredictDataDto> dbPredictResultData = null;
            var notRunningResult = predictDatas.Where(p => p.PredictedResult != 2).ToList();
            var notRunningResultCount = notRunningResult.Count();
            if (notRunningResultCount < lookupPeriodCount)
            {
                dbPredictResultData =
                    _lotteryPredictDataQueryService.GetNormHostoryPredictDatas(normId, planNormTable, lookupPeriodCount - notRunningResultCount, lotteryCode);
            }
            var count = 0;
            foreach (var item in notRunningResult)
            {
                count++;
                historyPredictResults.Add((int)item.PredictedResult);
                if (count >= lookupPeriodCount)
                {
                    break;
                }
            }
            foreach (var item in dbPredictResultData.Safe())
            {
                historyPredictResults.Add((int)item.PredictedResult);
            }
            return historyPredictResults.ToArray();
        }

        private void WritePlanTrackNumbers(IGrouping<string, PredictDataDto> item, PlanInfoDto planInfo, double currentScore, string lotteryCode)
        {
            var finalPredictData = _lotteryPredictDataQueryService.GetLastPredictData(item.Key, planInfo.PlanNormTable, lotteryCode);

            IList<PredictDataDto> needWritePredictDatas = null;
            needWritePredictDatas = finalPredictData != null ?
                item.Where(p => p.StartPeriod >= finalPredictData.StartPeriod).ToList()
                : item.Where(p => true).ToList();

            foreach (var predictData in needWritePredictDatas.Safe())
            {
                SendCommandAsync(new PredictDataCommand(Guid.NewGuid().ToString(), predictData.NormConfigId,
                    predictData.CurrentPredictPeriod, predictData.StartPeriod, predictData.EndPeriod,
                    predictData.MinorCycle, predictData.PredictedData,
                    predictData.PredictedResult, currentScore,
                    SystemUser, planInfo.PlanNormTable, lotteryCode, false));
            }
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

        #endregion private methods
    }
}