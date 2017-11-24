using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Infrastructure
{
    public class ConfigSettings
    {
        public static int NameServerPort { get; private set; }
        public static int BrokerProducerPort { get; private set; }
        public static int BrokerConsumerPort { get; private set; }
        public static int BrokerAdminPort { get; private set; }

        public static string NameServerIp { get; private set; }

        public static string BrokerProducerServiceIp { get; private set; }

        public static string BrokerConsumerServiceIp { get; private set; }

        public static string BrokerAdminServiceIp { get; private set; }


        public static string EqueueStorePath { get;private set; }

        public static void Initialize()
        {
            NameServerIp = ConfigurationManager.AppSettings["NameServerIp"] ?? IPAddress.Loopback.ToString();
            BrokerProducerServiceIp = ConfigurationManager.AppSettings["BrokerProducerServiceIp"] ?? IPAddress.Loopback.ToString();
            BrokerConsumerServiceIp = ConfigurationManager.AppSettings["BrokerConsumerServiceIp"] ?? IPAddress.Loopback.ToString();
            BrokerAdminServiceIp = ConfigurationManager.AppSettings["BrokerAdminServiceIp"] ?? IPAddress.Loopback.ToString();

            EqueueStorePath = ConfigurationManager.AppSettings["EqueueStorePath"];

            NameServerPort = 10191;
            BrokerProducerPort = 10192;
            BrokerConsumerPort = 10193;
            BrokerAdminPort = 10194;
        }


    }
}
