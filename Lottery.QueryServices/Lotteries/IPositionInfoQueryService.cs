using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface IPositionInfoQueryService
    {
        ICollection<PositionInfoDto> GetAll();

        ICollection<PositionInfoDto> GetLotteryPositions(string lotteryId);
    }
}