using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECommon.Components;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Plan;
using Lottery.Engine;
using Lottery.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class LotteryCalculateTest : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void GetLotteryEngine()
        {
            var lotteryId = "ACB89F4E-7C71-4785-BA09-D7E73084B467";

            var lotteryEngine = EngineContext.LotterEngine(lotteryId);
            var numberPredictor = lotteryEngine.GetPerdictor(AlgorithmType.DiscreteMarkov);

            List<int> data = new List<int>();
          
            Debug.Assert(lotteryEngine != null);
        }

        [TestMethod]
        public void PlanInfo_Test()
        {
            var planService = ObjectContainer.Resolve<IPlanInfoAppService>();
            var lotteryDataService = ObjectContainer.Resolve<ILotteryDataAppService>();

            var planInfo = planService.GetPlanInfo("WUMC");
            var datas = lotteryDataService.LotteryDataList("ACB89F4E-7C71-4785-BA09-D7E73084B467");

            var dataPositons = datas.LotteryDatas(1);

            Debug.Assert(planInfo != null && planInfo.PositionInfos.Any() && planInfo.LotteryInfo != null);
        }
    }
}