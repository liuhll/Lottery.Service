using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Infrastructure
{
    public class ServiceConfigSettings
    {

       
        public static string BrokerProducerServiceAddress { get; private set; }

        public static string BrokerConsumerServiceAddress { get; private set; }

        public static string BrokerAdminServiceAddress { get; private set; }

        public static string EqueueStorePath { get;private set; }

        public static IList<IPEndPoint> NameServerEndpoints { get; private set; }


        public static void Initialize()
        {
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
            var nameServerAddressStr = ConfigurationManager.AppSettings["NameServerAddressList"].Split(';');
            foreach (var address in nameServerAddressStr)
            {
                var ipEndpoint = address.Split(':');
                var nameServerAddress = new IPEndPoint(IPAddress.Parse(ipEndpoint[0]), int.Parse(ipEndpoint[1]));
                NameServerEndpoints.Add(nameServerAddress);
            }

            BrokerProducerServiceAddress = ConfigurationManager.AppSettings["BrokerProducerServiceAddress"];
            BrokerConsumerServiceAddress = ConfigurationManager.AppSettings["BrokerConsumerServiceAddress"];
            BrokerAdminServiceAddress = ConfigurationManager.AppSettings["BrokerAdminServiceAddress"];
            EqueueStorePath = ConfigurationManager.AppSettings["EqueueStorePath"];
        

    }



    }
}
