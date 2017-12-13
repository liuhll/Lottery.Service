using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommon.Components;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;

namespace Lottery.RunApp.Services
{
    [Component]
    public class LotteryPredictTableService : ILotteryPredictTableService
    {
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly ICommandService _commandService;

        public LotteryPredictTableService(ILotteryQueryService lotteryQueryService, IPlanInfoQueryService planInfoQueryService, ICommandService commandService)
        {
            _lotteryQueryService = lotteryQueryService;
            _planInfoQueryService = planInfoQueryService;
            _commandService = commandService;
        }

        public void InitLotteryPredictTables()
        {
            var lotteryInfos = _lotteryQueryService.GetAllLotteryInfo();
            foreach (var lotteryInfo in lotteryInfos)
            {
                if (!lotteryInfo.IsCompleteDynamicTable)
                {
                    InitLotteryPredictTable(lotteryInfo.Id).Wait();
                }
            }
        }

        private async Task InitLotteryPredictTable(string lotteryId)
        {
            var lotteryPlans = _planInfoQueryService.GetPlanInfoByLotteryId(lotteryId);
            var predictTables = lotteryPlans.Select(p => p.PlanNormTable).ToList();
            var predictDbName = AanalyseDbName();
            var result =await _commandService.SendAsync(new InitPredictTableCommand(Guid.NewGuid().ToString(), predictDbName, predictTables));
            if (result.Status == AsyncTaskStatus.Success)
            {
                await _commandService.SendAsync(new CompleteDynamicTableCommand(lotteryId,true));
            }
        }

        private string AanalyseDbName()
        {
            var connectionSettings = DataConfigSettings.ForecastLotteryConnectionString.Split(';');
            var dbNameSetting = connectionSettings.First(p => p.ToLower().Contains("database"));

            return dbNameSetting.Split('=')[1];
        }
    }
}