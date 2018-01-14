using System;
using ECommon.Components;
using Lottery.Commands.Norms;
using Lottery.QueryServices.UserInfos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class Norm_Test : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();      
        }

        [TestMethod]
        public void AddUserDefaultNorm()
        {
            ExecuteCommand(new AddUserNormDefaultConfigCommand(Guid.NewGuid().ToString(),
                "08b4c537-08aa-40f9-9d24-ab6ccd1b189c", "ACB89F4E-7C71-4785-BA09-D7E73084B467", 3, 3, 1, 10, 10, 1, 10,
                10, 1, 11));
        }
    }
}