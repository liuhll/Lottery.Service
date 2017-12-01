
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryQueryService
    {
        LotteryInfoDto GetLotteryInfoByCode(string lotteryCode);

        ICollection<LotteryInfoDto> GetAllLotteryInfo();


    }
}