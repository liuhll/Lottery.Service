using System;
using ECommon.Components;
using ECommon.Utilities;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.Commands.LotteryDatas;
using Lottery.Dtos.Lotteries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class LotteryDataTest : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void Insert_LotteryData_Test()
        {
            var lotteryId = "ACB89F4E-7C71-4785-BA09-D7E73084B467";
            var id = ObjectId.GenerateNewStringId();
            var lotteryData = "1,2,3,4,5,6,7,8,9,10";
            var insertTime = DateTime.Now;
            var period = 1000;

            var result = ExecuteCommand(new AddLotteryDataCommand(Guid.NewGuid().ToString(), new LotteryDataDto()
            {
                Data = lotteryData,
                LotteryId = lotteryId,
                LotteryTime = DateTime.Now,
                Period = 1000,
            }));

            Assert.AreEqual(CommandStatus.Success, result.Status);

        }

        [TestMethod]
        public void New_Predict_Data_Test()
        {
            var lotteryDataService = ObjectContainer.Resolve<ILotteryDataAppService>();

            var newPredictDatas =
                lotteryDataService.NewLotteryDataList("ACB89F4E-7C71-4785-BA09-D7E73084B467", 1100, "");
        }
    }
}