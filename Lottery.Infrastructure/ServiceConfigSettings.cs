using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;

namespace Lottery.Infrastructure
{
    public class ServiceConfigSettings
    {
        public static string BrokerProducerServiceAddress { get; private set; }

        public static string BrokerConsumerServiceAddress { get; private set; }

        public static string BrokerAdminServiceAddress { get; private set; }

        public static string EqueueStorePath { get; private set; }

        public static IList<IPEndPoint> NameServerEndpoints { get; private set; }

        public static IPEndPoint NameServerAddress { get; private set; }

        public static string BrokerGroup { get; private set; }

        public static string BrokerName { get; private set; }

        public static IPEndPoint CommandServiceprocessorAddress { get; private set; }

        public static void Initialize()
        {
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            // Change the minimum number of worker threads to four, but
            // keep the old setting for minimum asynchronous I/O
            // completion threads.
            ThreadPool.SetMinThreads(250, minIOC);
            //if (isDevEnv)
            //{
            //    NameServerEndpoints = new List<IPEndPoint>();
            //    NameServerEndpoints.Add(new IPEndPoint(IPAddress.Loopback,10091 ));
            //    BrokerProducerServiceAddress = new IPEndPoint(IPAddress.Loopback, 10092).ToString();
            //    BrokerConsumerServiceAddress = new IPEndPoint(IPAddress.Loopback, 10093).ToString();
            //    BrokerAdminServiceAddress = new IPEndPoint(IPAddress.Loopback, 10094).ToString();
            //}
            //else
            //{
            //    NameServerEndpoints = new List<IPEndPoint>();
            //    var nameServerAddressStr = ConfigurationManager.AppSettings["NameServerAddressList"].Split(';');
            //    foreach (var address in nameServerAddressStr)
            //    {
            //        var ipEndpoint = address.Split(':');
            //        var nameServerAddress = new IPEndPoint(IPAddress.Parse(ipEndpoint[0]), int.Parse(ipEndpoint[1]));
            //        NameServerEndpoints.Add(nameServerAddress);
            //    }

            //    BrokerProducerServiceAddress = ConfigurationManager.AppSettings["BrokerProducerServiceAddress"];
            //    BrokerConsumerServiceAddress = ConfigurationManager.AppSettings["BrokerConsumerServiceAddress"];
            //    BrokerAdminServiceAddress = ConfigurationManager.AppSettings["BrokerAdminServiceAddress"];
            //    EqueueStorePath = ConfigurationManager.AppSettings["EqueueStorePath"];
            //}
            NameServerEndpoints = new List<IPEndPoint>();
            var nameServerAddressListStr = ConfigurationManager.AppSettings["NameServerAddressList"].Split(';');
            foreach (var address in nameServerAddressListStr)
            {
                var ipEndpoint = address.Split(':');
                var nameServerAddress = new IPEndPoint(IPAddress.Parse(ipEndpoint[0]), int.Parse(ipEndpoint[1]));
                NameServerEndpoints.Add(nameServerAddress);
            }
            var nameServerAddressStr = ConfigurationManager.AppSettings["NameServerServerAddress"].Split(':');
            NameServerAddress = new IPEndPoint(IPAddress.Parse(nameServerAddressStr[0]), int.Parse(nameServerAddressStr[1]));

            BrokerProducerServiceAddress = ConfigurationManager.AppSettings["BrokerProducerServiceAddress"];
            BrokerConsumerServiceAddress = ConfigurationManager.AppSettings["BrokerConsumerServiceAddress"];
            BrokerAdminServiceAddress = ConfigurationManager.AppSettings["BrokerAdminServiceAddress"];
            EqueueStorePath = ConfigurationManager.AppSettings["EqueueStorePath"];
            BrokerGroup = ConfigurationManager.AppSettings["BrokerGroup"];
            BrokerName = ConfigurationManager.AppSettings["BrokerName"];

            var commandServiceprocessorAddressStr = ConfigurationManager.AppSettings["CommandServiceProcessorAddress"].Split(':');
            CommandServiceprocessorAddress = new IPEndPoint(IPAddress.Parse(commandServiceprocessorAddressStr[0]), int.Parse(commandServiceprocessorAddressStr[1]));
        }
    }
}