using ECommon.Components;
using Effortless.Net.Encryption;
using ENode.Commanding;
using Lottery.Commands.UserInfos;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.UserInfos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

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
        }

        [TestMethod]
        public void CreateUser_Test()
        {
            var account = "liuhl3";
            var pwd = "123qwe";
            var accountRegType = ReferAccountRegType(account);
            var userInfoCommand = new AddUserInfoCommand(Guid.NewGuid().ToString(), account, EncryptPassword(account, pwd, accountRegType),
                ClientRegistType.Web, accountRegType, 0);

            var commandResult = ExecuteCommand(userInfoCommand);
            Assert.AreEqual(commandResult.Status, CommandStatus.Success);
        }

        [TestMethod]
        public void BindUserProfile_Test()
        {
            var userProfile = "13292929292@qq.com";
            var userId = "08b4c537-08aa-40f9-9d24-ab6ccd1b189c";

            var userProfileCommand = new BindUserEmailCommand(userId, userProfile);

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