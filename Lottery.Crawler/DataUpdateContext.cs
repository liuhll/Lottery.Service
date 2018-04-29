using ECommon.Components;
using Lottery.QueryServices.Lotteries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Lottery.Crawler
{
    public class DataUpdateContext
    {
        private static readonly IDictionary<string, IList<IDataUpdateItem>> _lotteryDataUpdateItems;

        static DataUpdateContext()
        {
            _lotteryDataUpdateItems = new Dictionary<string, IList<IDataUpdateItem>>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Initialize()
        {
            var lotteryInfoQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            var dataSiteQueryService = ObjectContainer.Resolve<IDataSiteQueryService>();
            var lotteryInfos = lotteryInfoQueryService.GetAllLotteryInfo();

            foreach (var lotteryInfo in lotteryInfos)
            {
                _lotteryDataUpdateItems[lotteryInfo.Id] = new List<IDataUpdateItem>();
                var dataSites = dataSiteQueryService.GetDataSites(lotteryInfo.Id);
                foreach (var dataSite in dataSites)
                {
                    if (dataSite.Status)
                    {
                        var dataUpdateItemType = Type.GetType(dataSite.CrawlType);
                        Debug.Assert(dataUpdateItemType != null, "dataUpdateItemType != null");

                        var dataUpdateItem =
                            Activator.CreateInstance(dataUpdateItemType, new[] { dataSite }) as IDataUpdateItem;
                        _lotteryDataUpdateItems[lotteryInfo.Id].Add(dataUpdateItem);
                    }
                }
            }
        }

        public static IList<IDataUpdateItem> GetDataUpdateItems(string lotteryId)
        {
            return _lotteryDataUpdateItems[lotteryId];
        }
    }
}