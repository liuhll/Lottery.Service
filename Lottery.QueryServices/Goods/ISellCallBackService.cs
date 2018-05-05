using Lottery.Dtos.Account;
using Lottery.Dtos.Sells;

namespace Lottery.QueryServices.Goods
{
    public interface ISellCallBackService
    {
        void PayCallBack(NotifyCallBackInput input, UserBaseDto userInfo,out string lotteryId);
        string PointPay(PointPayInput input, string userId);
    }
}