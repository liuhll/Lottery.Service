using System.Diagnostics;
using Lottery.Engine;
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
            var numberPredictor = lotteryEngine.GetPerdictor("num");
            Debug.Assert(lotteryEngine != null);
        }
    }
}