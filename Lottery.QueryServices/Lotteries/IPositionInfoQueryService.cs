using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface IPositionInfoQueryService
    {
        ICollection<PositionInfoDto> GetAll();
    }
}