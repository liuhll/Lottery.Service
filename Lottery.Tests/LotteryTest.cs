using System;
using ECommon.Utilities;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class LotteryTest : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void Instert_LotteryData_Test()
        {
            var lotteryId = "ACB89F4E-7C71-4785-BA09-D7E73084B467";
            var id = ObjectId.GenerateNewStringId();
            var lotteryData = "1,2,3,4,5,6,7,8,9,10";
            var insertTime = DateTime.Now;
            var period = 1000;

            var result = ExecuteCommand(new NewLotteryCommand(id, period, lotteryId, lotteryData, insertTime));

            Assert.AreEqual(CommandStatus.Success,result.Status);


        }
    }
}
