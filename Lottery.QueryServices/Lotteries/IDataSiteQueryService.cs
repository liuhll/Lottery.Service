using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface IDataSiteQueryService
    {
        ICollection<DataSiteDto> GetDataSites(string lotteryId);
    }
}