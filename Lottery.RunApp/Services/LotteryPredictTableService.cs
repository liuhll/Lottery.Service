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
using Lottery.Dtos.Lotteries;
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
                    InitLotteryPredictTable(lotteryInfo).Wait();
                }
            }
        }

        private async Task InitLotteryPredictTable(LotteryInfoDto lotteryInfo)
        {
            var lotteryPlans = _planInfoQueryService.GetPlanInfoByLotteryId(lotteryInfo.Id);
            var predictTables = lotteryPlans.Select(p => p.PlanNormTable).ToList();
            var predictDbName = AanalyseDbName(lotteryInfo.LotteryCode);
            var result = await _commandService.SendAsync(new InitPredictTableCommand(Guid.NewGuid().ToString(), predictDbName,lotteryInfo.LotteryCode, predictTables));
            if (result.Status == AsyncTaskStatus.Success)
            {
                await _commandService.SendAsync(new CompleteDynamicTableCommand(lotteryInfo.Id, true));
            }
        }

        private string AanalyseDbName(string lotteryCode)
        {
            var connectionSettings = DataConfigSettings.ForecastLotteryConnectionString.Split(';');
            var dbNameSetting = connectionSettings.First(p => p.ToLower().Contains("database"));

            return string.Format(dbNameSetting.Split('=')[1], lotteryCode);
        }
    }
}