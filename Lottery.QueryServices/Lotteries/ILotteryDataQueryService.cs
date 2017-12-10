using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryDataQueryService
    {
        ICollection<LotteryDataDto> GetAllDatas(string lotteryId, int count = 10000);

    }
}