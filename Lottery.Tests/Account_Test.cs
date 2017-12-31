using System;
using ECommon.Components;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.UserInfos;
using Lottery.QueryServices.UserInfos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class Account_Test : TestBase
    {
        private static IUserTicketService _userTicketService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
            _userTicketService = ObjectContainer.Resolve<IUserTicketService>();
        }


        [TestMethod]
        public void Login_Test()
        {
            var id = Guid.NewGuid().ToString();
            var addUserTicketCommand = new AddAccessTokenCommand(id, "90E9E150-DC37-4B81-B1E6-3441B5D74331","testToken", "90E9E150-DC37-4B81-B1E6-3441B5D74331");
            ExecuteCommand(addUserTicketCommand);

            var userTicket = _userTicketService.GetValidTicketInfo("90E9E150-DC37-4B81-B1E6-3441B5D74331").Result;
            ExecuteCommand(new UpdateAccessTokenCommand(userTicket.Id, "90E9E150-DC37-4B81-B1E6-3441B5D74331",
                "testac2", "90E9E150-DC37-4B81-B1E6-3441B5D74331"));

        }
    }
}