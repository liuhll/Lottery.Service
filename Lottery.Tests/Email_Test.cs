using ECommon.Components;
using Lottery.Infrastructure.Mail;
using Lottery.QueryServices.UserInfos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Tests
{
    [TestClass]
    public class Email_Test : TestBase
    {
        private static IEmailSender _emailSender;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Initialize();
            _emailSender = ObjectContainer.Resolve<IEmailSender>();
        }

        [TestMethod]
        public void SendEmail()
        {
            _emailSender.Send("1029765111@qq.com","测试邮件发送组件", "测试邮件发送是否正常");
        }
    }
}