using ENode.Commanding;
using Lottery.AppService.Sell;
using Lottery.Commands.Sells;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ECommon.Extensions;
using Lottery.AppService.Account;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Norm;
using Lottery.AppService.Plan;
using Lottery.Commands.Norms;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Json;
using Lottery.QueryServices.Norms;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/sell")]
    public class SellController : BaseApiV1Controller
    {
        private readonly ISellAppService _sellAppService;
        private readonly IUserManager _userManager;
        private readonly INormConfigAppService _normConfigAppService;
        private readonly IPlanInfoAppService _planInfoAppService;
        private readonly INormPlanConfigQueryService _normPlanConfigQueryService;
        private readonly IUserNormDefaultConfigService _userNormDefaultConfigService;
        private readonly ILotteryDataAppService _lotteryDataAppService;

        public SellController(ICommandService commandService,
            ISellAppService sellAppService,
            IUserManager userManager, 
            INormConfigAppService normConfigAppService,
            IPlanInfoAppService planInfoAppService, 
            INormPlanConfigQueryService normPlanConfigQueryService,
            IUserNormDefaultConfigService userNormDefaultConfigService,
            ILotteryDataAppService lotteryDataAppService) : base(commandService)
        {
            _sellAppService = sellAppService;
            _userManager = userManager;
            _normConfigAppService = normConfigAppService;
            _planInfoAppService = planInfoAppService;
            _normPlanConfigQueryService = normPlanConfigQueryService;
            _userNormDefaultConfigService = userNormDefaultConfigService;
            _lotteryDataAppService = lotteryDataAppService;
        }

        /// <summary>
        /// 获取销售类型
        /// </summary>
        /// <returns></returns>
        [Route("salestype")]
        [AllowAnonymous]
        public ICollection<SellTypeOutput> GetSalesType()
        {
            return _sellAppService.GetSalesType(_userMemberRank);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="sellType"></param>
        /// <returns></returns>
        [Route("goodslist")]
        [AllowAnonymous]
        public ICollection<GoodsOutput> GetGoodInfos(SellType sellType)
        {
            return _sellAppService.GetGoodsInfos(_userMemberRank, _lotterySession.SystemTypeId, sellType);
        }

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <returns></returns>
        [Route("userauth")]
        [HttpGet]
        [AllowAnonymous]
        public UserAuthOutput GetMyselfAuthInfo()
        {
            return _sellAppService.GetMyselfAuthInfo(_lotterySession.UserId, _lotterySession.SystemTypeId);
        }

        /// <summary>
        /// 购买下订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("order")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<OrderOutput> Order(OrderInput input)
        {
            var goods = _sellAppService.GetGoodsInfoById(input.GoodId,input.SellType);
            var discount = _sellAppService.GetDiscount(goods.AuthRankId, input.SellType);
            if (goods.Term.HasValue)
            {
                if (input.Count != goods.Term.Value && input.UnitPrice.Equals(goods.UnitPrice) &&
                    input.Discount.Equals(discount))
                {
                    throw new LotteryDataException("订单信息错误,核对您的订单");
                }
            }
            else
            {
                if (input.UnitPrice.Equals(goods.UnitPrice) &&
                    input.Discount.Equals(discount))
                {
                    throw new LotteryDataException("订单信息错误,核对您的订单");
                }
            }
            var orderInfo = GenerateOrder(input, goods, discount);
            await SendCommandAsync(orderInfo);

            var orderDic = new List<OrderInfoItem>();
            orderDic.Add(new OrderInfoItem()
            {
                Label = "订单号",
                Value = orderInfo.SalesOrderNo,
                Key = "SalesOrderNo",
            });
            orderDic.Add(new OrderInfoItem()
            {
                Label = "商品名称",
                Value = goods.GoodName,
                Key = "GoodName"
            });
            orderDic.Add(new OrderInfoItem()
            {
                Label = "授权版本",
                Value = ((MemberRank)goods.MemberRank).GetChineseDescribe(),
                Key = "MemberRank"
            });

            orderDic.Add(new OrderInfoItem()
            {
                Label = "有效期",
                Value = DateTime.Now.AddMonths(orderInfo.Count).ToString("yyyy-MM-dd HH:mm:ss"),
                Key = "ValidDate"
            });

            if (!discount.Equals(1.00))
            {
                orderDic.Add(new OrderInfoItem()
                {
                    Label = "原价",
                    Value = orderInfo.OriginalCost.ToString("0.00"),
                    Key = "OriginalCost"
                });

                orderDic.Add(new OrderInfoItem()
                {
                    Label = "折扣",
                    Value = discount.ToString("0.00"),
                    Key = "discount"
                });

            }

            var output = new OrderOutput()
            {
                OrderPrice = orderInfo.OrderCost,
                OrderInfo = orderDic,
                OrderNo = orderInfo.SalesOrderNo,
            };
            return output;
        }

        /// <summary>
        /// 根据订单Id获取订单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("order")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<OrderOutput> GetOrder(string orderNo)
        {
            var orderInfo = _sellAppService.GetOrderInfo(orderNo);
            var goods = _sellAppService.GetGoodsInfoById(orderInfo.GoodsId, SellType.Rmb);
            var discount = orderInfo.OrderCost / orderInfo.OriginalCost;
            var orderDic = new List<OrderInfoItem>();
            orderDic.Add(new OrderInfoItem()
            {
                Label = "订单号",
                Value = orderInfo.SalesOrderNo,
                Key = "SalesOrderNo",
            });
            orderDic.Add(new OrderInfoItem()
            {
                Label = "商品名称",
                Value = goods.GoodName,
                Key = "GoodName"
            });
            orderDic.Add(new OrderInfoItem()
            {
                Label = "授权版本",
                Value = ((MemberRank)goods.MemberRank).GetChineseDescribe(),
                Key = "MemberRank"
            });

            orderDic.Add(new OrderInfoItem()
            {
                Label = "有效期",
                Value = DateTime.Now.AddMonths(orderInfo.Count).ToString("yyyy-MM-dd HH:mm:ss"),
                Key = "ValidDate"
            });

            if (!discount.Equals(1.00))
            {
                orderDic.Add(new OrderInfoItem()
                {
                    Label = "原价",
                    Value = orderInfo.OriginalCost.ToString("0.00"),
                    Key = "OriginalCost"
                });

                orderDic.Add(new OrderInfoItem()
                {
                    Label = "折扣",
                    Value = discount.ToString("0.00"),
                    Key = "discount"
                });

            }

            var output = new OrderOutput()
            {
                OrderPrice = orderInfo.OrderCost,
                OrderInfo = orderDic,
                OrderNo = orderInfo.SalesOrderNo,
            };
            return output;

        }

        /// <summary>
        /// 调起支付接口
        /// </summary>
        /// <param name="input">支付订单信息</param>
        /// <returns></returns>
        [Route("pay")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<PayOutput> Pay(PayInput input)
        {
            var orderInfo = _sellAppService.GetOrderInfo(input.OrderId);
            if (orderInfo == null)
            {
                throw new LotteryDataException("下单失败,请稍后重试");
            }
            if (!orderInfo.OrderCost.Equals(input.Price))
            {
                throw new LotteryDataException("订单金额错误,请核对订单信息");
            }
            if (input.IsType != PayType.AliPay && input.IsType != PayType.Wechat)
            {
                throw new LotteryDataException("请选择支付方式");
            }
            var paysApiInfo = _sellAppService.GetPaysApiInfo();
            var payInfo = new PayOrderDto()
            {
                Uid = paysApiInfo.Uid,
                Price = input.Price.ToString("#0.00"),
                Istype = (int)input.IsType,
              //  Goodsname = input.GoodsName,
                Notify_url = paysApiInfo.NotifyUrl,
                Return_url = paysApiInfo.ReturnUrl,
                Orderid = input.OrderId,
                Orderuid = _lotterySession.UserName,

            };
            payInfo.Key = GetPayKey(payInfo, paysApiInfo.Token);

            return _sellAppService.GetPayOrderInfo(payInfo, paysApiInfo.PaysApi);
        }

        /// <summary>
        /// 支付回调接口(PaysApi)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("notify")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Notify(NotifyCallBackInput input)
        {
            if (input == null)
            {
                throw  new HttpException("回调参数不允许为null");
            }
#if !DEBUG
            var callKey = GetNotifyCallBackKey(input);
            if (!callKey.Equals(input.Key))
            {
                throw new HttpException("密钥不正确,回调可能不是由PaysApi发起");
            }

#endif

            var userInfo = await _userManager.GetAccountBaseInfo(input.Orderuid);
            string lotteryId = string.Empty;
            var result = _sellAppService.PayCallBack(input, userInfo, out lotteryId);

            if (!result)
            {
                throw new HttpException("业务处理失败");
            }

            var defaultUserNorms = _normConfigAppService.GetNormConfigsByUserIdOrDefault(lotteryId);

            var userNormConfigs = _normConfigAppService.GetUserNormConfig(lotteryId, userInfo.Id);
            var userDefaultNormConfig =
                _userNormDefaultConfigService.GetUserNormOrDefaultConfig(userInfo.Id, lotteryId);
            var finalLotteryData = _lotteryDataAppService.GetFinalLotteryData(lotteryId);

            if (!userNormConfigs.Safe().Any())
            {
                foreach (var userNorm in defaultUserNorms)
                {
                    // 如果用户选中的计划则忽略
                    if (userNormConfigs != null && userNormConfigs.Any(p => p.PlanId == userNorm.PlanId))
                    {
                        continue;
                    }
                    var planInfo = _planInfoAppService.GetPlanInfoById(userNorm.PlanId);
                    var planNormConfigInfo =
                        _normPlanConfigQueryService.GetNormPlanDefaultConfig(planInfo.LotteryInfo.LotteryCode,
                            planInfo.PredictCode);
                    var planCycle = userNorm.PlanCycle;
                    var forecastCount = userNorm.ForecastCount;
                    if (planNormConfigInfo != null)
                    {
                        if (planCycle > planNormConfigInfo.MaxPlanCycle)
                        {
                            planCycle = planNormConfigInfo.MaxPlanCycle;
                        }
                        if (forecastCount > planNormConfigInfo.MaxForecastCount)
                        {
                            forecastCount = planNormConfigInfo.MaxForecastCount;
                        }
                    }

                    var command = new AddNormConfigCommand(Guid.NewGuid().ToString(), userInfo.Id,
                        lotteryId, planInfo.Id, planCycle,
                        forecastCount, finalLotteryData.Period,
                        userDefaultNormConfig.UnitHistoryCount, userDefaultNormConfig.HistoryCount,
                        userDefaultNormConfig.MinRightSeries,
                        userDefaultNormConfig.MaxRightSeries, userDefaultNormConfig.MinErrorSeries,
                        userDefaultNormConfig.MaxErrorSeries, userDefaultNormConfig.LookupPeriodCount,
                        userDefaultNormConfig.ExpectMinScore, userDefaultNormConfig.ExpectMaxScore, userNorm.Sort);
                    await SendCommandAsync(command);
                }
            }
            return "OK";
        }

        private string GetNotifyCallBackKey(NotifyCallBackInput input)
        {
            var paysApiInfo = _sellAppService.GetPaysApiInfo();
            var keyLine = input.Orderid + input.Orderuid + input.Paysapi_id + input.Price + input.Realprice +
                          paysApiInfo.Token;
            return EncryptHelper.Md5(keyLine);
        }

        private string GetPayKey(PayOrderDto payInfo, string token)
        {

            var keyLine = string.Empty; // payInfo.Goodsname + payInfo.Istype + payInfo.Notify_url + payInfo.Orderid + payInfo.Orderuid
                                        //+ payInfo.Price + payInfo.Return_url + token + payInfo.Uid;
            if (!payInfo.Goodsname.IsNullOrEmpty())
            {
                keyLine += payInfo.Goodsname;
            }

            keyLine += payInfo.Istype;

            if (!payInfo.Notify_url.IsNullOrEmpty())
            {
                keyLine += payInfo.Notify_url;
            }
            if (!payInfo.Orderid.IsNullOrEmpty())
            {
                keyLine += payInfo.Orderid;
            }
            if (!payInfo.Orderuid.IsNullOrEmpty())
            {
                keyLine += payInfo.Orderuid;
            }
            keyLine += payInfo.Price;
            if (!payInfo.Return_url.IsNullOrEmpty())
            {
                keyLine += payInfo.Return_url;
            }
            keyLine += token;
            if (!payInfo.Uid.IsNullOrEmpty())
            {
                keyLine += payInfo.Uid;
            }

            return EncryptHelper.Md5(keyLine);
        }

        private AddOrderRecordCommand GenerateOrder(OrderInput input, GoodsInfoDto goods, double discount)
        {
            var orderNo = OrderHelper.GenerateOrderNo(OrderType.Order, input.SellType);
            var orderOriginCost = input.Count * input.UnitPrice;
            var orderCost = orderOriginCost * discount;
            return new AddOrderRecordCommand(Guid.NewGuid().ToString(), orderNo,goods.Id, goods.AuthRankId, _lotterySession.SystemTypeId, OrderSourceType.V1,
                input.Count, input.UnitPrice, orderOriginCost, orderCost, input.SellType, _lotterySession.UserId);
        }
    }
}