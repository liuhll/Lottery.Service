using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryQueryService
    {
        LotteryInfoDto GetLotteryInfoByCode(string lotteryCode);

        ICollection<LotteryInfoDto> GetAllLotteryInfo();

        LotteryInfoDto GetLotteryInfoById(string lotteryId);
    }
}