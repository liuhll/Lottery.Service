using System;
using System.Collections.Generic;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;


namespace Lottery.QueryServices.Goods
{
    public interface ISellQueryService 
    {
        IList<GoodsInfoDto> GetRmbGoodInfos(MemberRank memberRank, string lotteryId);
        IList<GoodsInfoDto> GetPointGoodInfos(MemberRank memberRank, string lotteryId);
        UserAuthDto GetUserAuthInfo(string userId, string lotteryId);
        UserAuthOutput GetMyselfAuthInfo(string userId, string lotteryId);
    }
}
