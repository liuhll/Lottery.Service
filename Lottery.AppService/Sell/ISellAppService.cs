using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;
using Lottery.Dtos.Account;

namespace Lottery.AppService.Sell
{
    public interface ISellAppService
    {
        ICollection<SellTypeOutput> GetSalesType(MemberRank lotterySessionMemberRank);

        ICollection<GoodsOutput> GetGoodsInfos(MemberRank memberRank, string lotteryId, SellType sellType);

        UserAuthOutput GetMyselfAuthInfo(string userId, string lotteryId);

        GoodsInfoDto GetGoodsInfoById(string goodId, SellType sellType);

        double GetDiscount(string authRankId, SellType sellType);

        OrderInfoDto GetOrderInfo(string orderNo);

        PaysApiInfo GetPaysApiInfo();
        PayOutput GetPayOrderInfo(PayOrderDto payInfo,string payApi);
        bool PayCallBack(NotifyCallBackInput input, UserBaseDto userInfo, out string lotteryId);
        bool PointPay(PointPayInput input, out string lotteryId);
    }
}