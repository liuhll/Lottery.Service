using ECommon.Components;
using Lottery.Commands.Norms;
using Lottery.Core.Domain.PlanInfos;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.Norms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lottery.Tests
{
    [TestClass]
    public class Norm_Test : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void AddUserDefaultNormTest()
        {
            ExecuteCommand(new AddUserNormDefaultConfigCommand(Guid.NewGuid().ToString(),
                "08b4c537-08aa-40f9-9d24-ab6ccd1b189c", "ACB89F4E-7C71-4785-BA09-D7E73084B467", 3, 3, 1, 10, 10, 1, 10, 50,
                10, 1, 11));
        }

        [TestMethod]
        public void UpdateUserDefaultNormTest()
        {
            ExecuteCommand(new UpdateUserNormDefaultConfigCommand("a02eb7d6-e738-4812-b5b8-e302ba84f69c",
                3, 3, 1, 10, 10, 1, 10, 50,
                10, 1, 11));
        }

        [TestMethod]
        public void UpdateUserPlanTest()
        {
            var userId = "08b4c537-08aa-40f9-9d24-ab6ccd1b189c";
            var lotteryId = "ACB89F4E-7C71-4785-BA09-D7E73084B467";
            var planIds = new string[] { "83FF7434-C88E-45CD-A39F-73B3EB500001", "83FF7434-C88E-45CD-A39F-73B3EB500002" };
            var userDefaultNormConfigService = ObjectContainer.Resolve<IUserNormDefaultConfigService>();
            var finalLotteryDataService = ObjectContainer.Resolve<ILotteryFinalDataQueryService>();

            var userDefaultNormConfig = userDefaultNormConfigService.GetUserNormOrDefaultConfig(userId, lotteryId);
            var finalLotteryData = finalLotteryDataService.GetFinalData(lotteryId);
            var userNormConfig = new List<UserPlanNormConfig>();
            int sort = 1;
            foreach (var planId in planIds)
            {
                var command = new AddNormConfigCommand(Guid.NewGuid().ToString(), userId, lotteryId, planId,
                    userDefaultNormConfig.PlanCycle, userDefaultNormConfig.ForecastCount, finalLotteryData.FinalPeriod,
                    userDefaultNormConfig.UnitHistoryCount, userDefaultNormConfig.HistoryCount,
                    userDefaultNormConfig.MinRightSeries,
                    userDefaultNormConfig.MaxRightSeries, userDefaultNormConfig.MinErrorSeries,
                    userDefaultNormConfig.MaxErrorSeries, userDefaultNormConfig.LookupPeriodCount,
                    userDefaultNormConfig.ExpectMinScore, userDefaultNormConfig.ExpectMaxScore, sort);
                sort++;
                ExecuteCommand(command);
            }
        }
    }
}