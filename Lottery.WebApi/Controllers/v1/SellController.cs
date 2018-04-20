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
using System.Threading.Tasks;
using System.Web.Http;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/sell")]
    public class SellController : BaseApiV1Controller
    {
        private readonly ISellAppService _sellAppService;

        public SellController(ICommandService commandService, ISellAppService sellAppService) : base(commandService)
        {
            _sellAppService = sellAppService;
        }

        /// <summary>
        /// 获取销售类型
        /// </summary>
        /// <returns></returns>
        [Route("salestype")]
        [AllowAnonymous]
        public ICollection<SellTypeOutput> GetSalesType()
        {
            return _sellAppService.GetSalesType(_lotterySession.MemberRank);
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
            return _sellAppService.GetGoodsInfos(_lotterySession.MemberRank, _lotterySession.SystemTypeId, sellType);
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
        public async Task<string> Order(OrderInput input)
        {
            var goods = _sellAppService.GetGoodsInfoById(input.GoodId);
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

            return "OK";
        }

        private AddOrderRecordCommand GenerateOrder(OrderInput input, GoodsInfoDto goods, double discount)
        {
            var orderNo = OrderHelper.GenerateOrderNo(OrderType.Order, input.SellType);
            var orderOriginCost = input.Count * input.UnitPrice;
            var orderCost = orderOriginCost * discount;
            return new AddOrderRecordCommand(Guid.NewGuid().ToString(), orderNo, goods.AuthRankId, _lotterySession.SystemTypeId, OrderSourceType.V1,
                input.Count, input.UnitPrice, orderOriginCost, orderCost, input.SellType, _lotterySession.UserId);
        }
    }
}