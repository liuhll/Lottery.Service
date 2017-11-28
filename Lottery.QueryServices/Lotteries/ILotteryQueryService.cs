
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryQueryService
    {
        LotteryInfoDto GetLotteryInfoByCode(string lotteryCode);

        ICollection<LotteryInfoDto> GetAllLotteryInfo();


    }
}