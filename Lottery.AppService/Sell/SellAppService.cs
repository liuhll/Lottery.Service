using System;
using ECommon.Components;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Activities;
using Lottery.QueryServices.Goods;
using Lottery.QueryServices.Lotteries;
using System.Collections.Generic;
using System.Diagnostics;
using EasyHttp.Http;
using ECommon.Logging;
using Lottery.Dtos.Account;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Json;
using Lottery.Infrastructure.Logs;

namespace Lottery.AppService.Sell
{
    [Component]
    public class SellAppService : ISellAppService
    {
        private readonly ISellQueryService _sellQueryService;
        private readonly ILotterySession _lotterySession;
        private readonly IActivityQueryService _activityQueryService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly HttpClient _httpClient;
        private readonly ISellCallBackService _sellCallBackService;
        private readonly ILogger _logger;

        public SellAppService(ISellQueryService sellQueryService,
            IActivityQueryService activityQueryService,
            ILotteryQueryService lotteryQueryService,
            ISellCallBackService sellCallBackService)
        {
            _sellQueryService = sellQueryService;
            _activityQueryService = activityQueryService;
            _lotteryQueryService = lotteryQueryService;
            _sellCallBackService = sellCallBackService;
            _lotterySession = NullLotterySession.Instance;
            _httpClient = new HttpClient();
            _logger = NullLotteryLogger.Instance;
        }

        public ICollection<SellTypeOutput> GetSalesType(MemberRank memberRank)
        {
            ICollection<SellTypeOutput> result = new List<SellTypeOutput>();
            switch (memberRank)
            {
                case MemberRank.Ordinary:
                case MemberRank.Senior:
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Point,
                        SellTypeName = "积分兑换"
                    });
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Rmb,
                        SellTypeName = "购买"
                    });
                    break;

                case MemberRank.Specialty:
                case MemberRank.Elite:
                    result.Add(new SellTypeOutput()
                    {
                        SellType = SellType.Rmb,
                        SellTypeName = "购买"
                    });
                    break;
            }
            return result;
        }

        public ICollection<GoodsOutput> GetGoodsInfos(MemberRank memberRank, string lotteryId, SellType sellType)
        {
            IList<GoodsOutput> result;
            switch (sellType)
            {
                case SellType.Point:
                    result = GetPointGoodInfos(memberRank, lotteryId);
                    break;

                case SellType.Rmb:
                    result = GetRmbGoodInfos(memberRank, lotteryId);
                    break;

                default:
                    throw new LotteryException("参数错误，不存在该销售类型");
            }
            return result;
        }

        public UserAuthOutput GetMyselfAuthInfo(string userId, string lotteryId)
        {
            var authInfo = _sellQueryService.GetMyselfAuthInfo(userId, lotteryId)
                ?? new UserAuthOutput()
                {
                    Notes = "普通版授权",
                    LotteryName = _lotteryQueryService.GetLotteryInfoById(_lotterySession.SystemTypeId).Name
                };
            return authInfo;
        }

        public GoodsInfoDto GetGoodsInfoById(string goodId, SellType sellType)
        {
            var goodInfo = _sellQueryService.GetGoodsInfoById(goodId,sellType);
            if (goodInfo == null)
            {
                throw new LotteryException("获取商品信息失败,请稍后重试");
            }
            return goodInfo;
        }

        private IList<GoodsOutput> GetRmbGoodInfos(MemberRank memberRank, string lotteryId)
        {
            var result = new List<GoodsOutput>();
            var goodOutputs = _sellQueryService.GetRmbGoodInfos(memberRank, lotteryId);
            var userAuthInfo = _sellQueryService.GetUserAuthInfo(_lotterySession.UserId, lotteryId);
            foreach (var goods in goodOutputs)
            {
                Debug.Assert(goods.Term.HasValue);
                var output = new GoodsOutput()
                {
                    GoodsId = goods.Id,
                    GoodsName = goods.GoodName,
                    Count = goods.Term.Value,
                    Discount = GetDiscount(goods.AuthRankId, SellType.Rmb),
                    PurchaseType = GetPurchaseType(userAuthInfo, memberRank, goods.MemberRank),
                    UnitPrice = goods.UnitPrice
                };
                result.Add(output);
            }
            return result;
        }

        private PurchaseType GetPurchaseType(UserAuthDto userAuthInfo, MemberRank userMemberRank, int memberRank)
        {
            if (userAuthInfo == null)
            {
                return PurchaseType.New;
            }
            else if ((int)userMemberRank == memberRank)
            {
                return PurchaseType.Continuation;
            }
            else
            {
                return PurchaseType.Upgrade;
            }
        }

        public double GetDiscount(string authRankId, SellType sellType)
        {
            var activity = _activityQueryService.GetAuthAcivity(authRankId, sellType);
            if (activity == null)
            {
                return 1.00;
            }
            return activity.Discount;
        }

        public OrderInfoDto GetOrderInfo(string orderNo)
        {
            return _sellQueryService.GetOrderInfo(orderNo);
        }

        public PaysApiInfo GetPaysApiInfo()
        {
            return _sellQueryService.GetPaysApiInfo();
        }

        public PayOutput GetPayOrderInfo(PayOrderDto payInfo, string payApi)
        {

            var formData = new Dictionary<string, object>();
            formData.Add("uid", payInfo.Uid);
            formData.Add("price", payInfo.Price);
            formData.Add("istype", payInfo.Istype);
            formData.Add("notify_url", payInfo.Notify_url);
            formData.Add("return_url", payInfo.Return_url);
            formData.Add("orderid", payInfo.Orderid);
            formData.Add("orderuid", payInfo.Orderuid);
            formData.Add("goodsname", payInfo.Goodsname);
            formData.Add("key", payInfo.Key);

            dynamic result;
            try
            {
                var response = _httpClient.Post(payApi, formData, null, new { format = "json" });
                result = response.RawText.ToObject();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                string payType = string.Empty;
                string recommendPaytype = string.Empty;
                if (payInfo.Istype == 1)
                {
                    payType = "支付宝";
                    recommendPaytype = "微信";
                }
                else
                {
                    payType = "微信";
                    recommendPaytype = "支付宝";
                }
                throw new LotteryException(string.Format("{0}支付系统异常,推荐使用{1}支付,或稍后重试", payType, recommendPaytype));
            }

            string qrcodeLink = result.data.qrcode.ToString();
            if (qrcodeLink.IsNullOrEmpty())
            {
                string payType = string.Empty;
                string recommendPaytype = string.Empty;
                if (payInfo.Istype == 1)
                {
                    payType = "支付宝";
                    recommendPaytype = "微信";
                }
                else
                {
                    payType = "微信";
                    recommendPaytype = "支付宝";
                }
                throw new LotteryException(string.Format("{0}支付系统异常,推荐使用{1}支付,或稍后重试",payType,recommendPaytype));
            }
            var output = new PayOutput()
            {
                IsType = result.data.istype,
                Msg = payInfo.Goodsname,
                QrCodeImageAddress = string.Format(LotteryConstants.QrCodeUrl,result.data.qrcode),
                QrCode = result.data.qrcode,
                RealPrice = result.data.realprice
            };
            return output;
        }

        public bool PayCallBack(NotifyCallBackInput input, UserBaseDto userInfo,out string lotteryId)
        {
            try
            {
                _logger.Debug("回调参数为:" + input.ToJsonString());
                _sellCallBackService.PayCallBack(input, userInfo,out lotteryId);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                lotteryId = "";
                return false;
            }
           
        }


        private IList<GoodsOutput> GetPointGoodInfos(MemberRank memberRank, string lotteryId)
        {
            var result = new List<GoodsOutput>();
            var goodOutputs = _sellQueryService.GetPointGoodInfos(memberRank, lotteryId);
            var userAuthInfo = _sellQueryService.GetUserAuthInfo(_lotterySession.UserId, lotteryId);
            foreach (var goods in goodOutputs)
            {
                var output = new GoodsOutput()
                {
                    GoodsId = goods.Id,
                    GoodsName = goods.GoodName,
                    Count = 1,
                    Discount = GetDiscount(goods.AuthRankId, SellType.Point),
                    PurchaseType = GetPurchaseType(userAuthInfo,memberRank, goods.MemberRank),
                    UnitPrice = goods.UnitPrice
                };
                result.Add(output);
            }
            return result;
        }
    }
}