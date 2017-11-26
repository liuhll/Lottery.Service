using System.Configuration;

namespace Lottery.Infrastructure
{
    public class DataConfigSettings
    {
        public static string ENodeConnectionString { get; private set; }
        public static string LotteryConnectionString { get; private set; }
        public static string ForecastLotteryConnectionString { get;private set; }

        public static void Initialize()
        {
            ENodeConnectionString = ConfigurationManager.ConnectionStrings["enode"].ConnectionString;
            LotteryConnectionString = ConfigurationManager.ConnectionStrings["lottery"].ConnectionString;
            ForecastLotteryConnectionString = ConfigurationManager.ConnectionStrings["forecastlottery"].ConnectionString;
        }
    }
}