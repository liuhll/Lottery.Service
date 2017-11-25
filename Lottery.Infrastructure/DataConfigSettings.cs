using System.Configuration;

namespace Lottery.Infrastructure
{
    public class DataConfigSettings
    {
        public static string ENodeConnectionString { get; private set; }

        public static void Initialize()
        {
            ENodeConnectionString = ConfigurationManager.ConnectionStrings["enode"].ConnectionString;
        }
    }
}