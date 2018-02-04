using System.Collections.Generic;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Sell
{
    public interface ISellAppService
    {
        ICollection<SellTypeOutput> GetSalesType(MemberRank lotterySessionMemberRank);
        ICollection<GoodInfoDto> GetGoodInfos(string userId, MemberRank memberRank, SellType sellType);
    }
}