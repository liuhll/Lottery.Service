using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface IDataSiteQueryService
    {
        ICollection<DataSiteDto> GetDataSites(string lotteryId);
    }
}