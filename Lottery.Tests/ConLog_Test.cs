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