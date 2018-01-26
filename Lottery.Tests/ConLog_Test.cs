using System;
using ECommon.Components;
using Lottery.Commands.LogonLog;
using Lottery.QueryServices.UserInfos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class ConLog_Test : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
            
        }

        [TestMethod]
        public void AddConLog_Test()
        {
            // SendCommand(new AddConLogCommand());
        }
    }
}