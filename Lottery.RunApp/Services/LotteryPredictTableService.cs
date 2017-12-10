using System;
using System.Linq;
using System.Threading;
using ECommon.Components;
using ECommon.Extensions;
using ENode.Commanding;
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
                    InitLotteryPredictTable(lotteryInfo.Id);
                }
            }
        }

        private void InitLotteryPredictTable(string lotteryId)
        {
            var lotteryPlans = _planInfoQueryService.GetPlanInfoByLotteryId(lotteryId);
            var predictTables = lotteryPlans.Select(p => p.PlanNormTable).ToList();
            var predictDbName = AanalyseDbName();
            var result = _commandService.Execute(new InitPredictTableCommand(Guid.NewGuid().ToString(), predictDbName, predictTables),50000);
        }

        private string AanalyseDbName()
        {
            var connectionSettings = DataConfigSettings.ForecastLotteryConnectionString.Split(';');
            var dbNameSetting = connectionSettings.First(p => p.ToLower().Contains("database"));

            return dbNameSetting.Split('=')[1];
        }
    }
}