using System;
using System.Text.RegularExpressions;
using ECommon.Components;
using ECommon.Extensions;
using Effortless.Net.Encryption;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.UserInfos;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
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
            var addUserTicketCommand = new AddAccessTokenCommand(id, "6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7", "testToken", "6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7");
            ExecuteCommand(addUserTicketCommand);

            var userTicket = _userTicketService.GetValidTicketInfo("6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7").WaitResult(500);

            ExecuteCommand(new UpdateAccessTokenCommand(userTicket.Id, "6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7",
                "testac2", "6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7"));

        }

        [TestMethod]
        public void CreateUser_Test()
        {
            var account = "test242";
            var pwd = "123qwe";
            var accountRegType = ReferAccountRegType(account);
            var userInfoCommand = new AddUserInfoCommand(Guid.NewGuid().ToString(), account, EncryptPassword(account, pwd, accountRegType),
                ClientRegistType.Web, accountRegType);

            var commandResult = ExecuteCommand(userInfoCommand);
            Assert.AreEqual(commandResult.Status,CommandStatus.Success);
           
        }

        [TestMethod]
        public void BindUserProfile_Test()
        {
            var userProfile = "13292929292";

            var userProfileCommand = new BindUserPhoneCommand("6e8c2fe6-a9eb-4fcd-b4b1-4127e1b3f8a7", userProfile);

            var commandResult = ExecuteCommand(userProfileCommand);
            Assert.AreEqual(commandResult.Status, CommandStatus.Success);

        }

        private string EncryptPassword(string userAccount, string userPassword, AccountRegistType accountRegType)
        {
            var pwd = Hash.Create(HashType.MD5, userAccount + userPassword, accountRegType.ToString(), true);
            return pwd;
        }

        private AccountRegistType ReferAccountRegType(string userAccount)
        {
            if (Regex.IsMatch(userAccount, RegexConstants.UserName))
            {
                return AccountRegistType.UserName;
            }
            if (Regex.IsMatch(userAccount, RegexConstants.Email))
            {
                return AccountRegistType.Email;
            }
            if (Regex.IsMatch(userAccount, RegexConstants.Phone))
            {
                return AccountRegistType.Phone;
            }
            throw new LotteryDataException("注册账号不合法");

        }
    }
}