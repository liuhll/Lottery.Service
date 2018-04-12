using System.Collections.Generic;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Sell
{
    public interface ISellAppService
    {
        ICollection<SellTypeOutput> GetSalesType(MemberRank lotterySessionMemberRank);
        ICollection<GoodsOutput> GetGoodsInfos(MemberRank memberRank,string lotteryId, SellType sellType);
    }
}