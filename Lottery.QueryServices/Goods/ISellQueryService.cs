using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;

namespace Lottery.QueryServices.Goods
{
    public interface ISellQueryService
    {
        IList<GoodsInfoDto> GetRmbGoodInfos(MemberRank memberRank, string lotteryId);

        IList<GoodsInfoDto> GetPointGoodInfos(MemberRank memberRank, string lotteryId);

        UserAuthDto GetUserAuthInfo(string userId, string lotteryId);

        UserAuthOutput GetMyselfAuthInfo(string userId, string lotteryId);

        GoodsInfoDto GetGoodsInfoById(string goodId, SellType sellType);
        OrderInfoDto GetOrderInfo(string orderNo);
        PaysApiInfo GetPaysApiInfo();
    }
}