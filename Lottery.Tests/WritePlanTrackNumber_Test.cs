using Lottery.Commands.LotteryPredicts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lottery.Tests
{
    [TestClass]
    public class WritePlanTrackNumber_Test : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void WritePlanTrackNumberTest()
        {
            ExecuteCommand(new PredictDataCommand(Guid.NewGuid().ToString(), "9C4D2017-2946-49C8-BE8E-BC6102FB27AC",
                664042, 664039, 664042, 4, "10,9,8,3", 1, 1.0, "88042e91-d923-46f0-9871-c0c626af7905", "LA_LotteryPredictData_DSIMDM", "BJPKS", false));
        }
    }
}