using ECommon.Components;
using Lottery.RunApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class PredictTableTest : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void InitPredictTableTest()
        {
            var lotteryPredictTableService = ObjectContainer.Resolve<ILotteryPredictTableService>();

            lotteryPredictTableService.InitLotteryPredictTables();
        }
    }
}