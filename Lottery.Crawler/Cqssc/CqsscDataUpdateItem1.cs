using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.Crawler.Cqssc
{
    public class CqsscDataUpdateItem1 : BaseDataUpdateItem
    {
        public CqsscDataUpdateItem1(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            return null;
        }
    }
}