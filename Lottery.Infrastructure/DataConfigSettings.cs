using System.Configuration;

namespace Lottery.Infrastructure
{
    public class DataConfigSettings
    {
        public static string ENodeConnectionString { get; private set; }
        public static string LotteryConnectionString { get; private set; }
        public static string ForecastLotteryConnectionString { get;private set; }

        public static string RedisServiceAddress { get; private set; }

        public static void Initialize()
        {
            //if (isDevEnv)
            //{
            //    ENodeConnectionString = "Data Source=127.0.0.1;Integrated Security=true;Initial Catalog=Forum_ENode;Connect Timeout=30;Min Pool Size=10;Max Pool Size=100;Async=True";
            //    LotteryConnectionString = "Data Source=127.0.0.1;Integrated Security=true;Initial Catalog=LotteryV01;Connect Timeout=30;Min Pool Size=10;Max Pool Size=100;Async=True";
            //    ForecastLotteryConnectionString = "Data Source=127.0.0.1;Integrated Security=true;Initial Catalog=ForecastLottery;Connect Timeout=30;Min Pool Size=10;Max Pool Size=100;Async=True";

            //}
            //else
            //{

            //    ENodeConnectionString = ConfigurationManager.ConnectionStrings["enode"].ConnectionString;
            //    LotteryConnectionString = ConfigurationManager.ConnectionStrings["lottery"].ConnectionString;
            //}

            ENodeConnectionString = ConfigurationManager.ConnectionStrings["enode"].ConnectionString;
            LotteryConnectionString = ConfigurationManager.ConnectionStrings["lottery"].ConnectionString;
            ForecastLotteryConnectionString = ConfigurationManager.ConnectionStrings["forecastlottery"].ConnectionString;

            RedisServiceAddress = ConfigurationManager.AppSettings["RedisServiceAddress"];
        }
          
    }
}