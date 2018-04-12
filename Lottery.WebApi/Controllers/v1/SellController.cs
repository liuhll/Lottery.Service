using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Sell;
using Lottery.Dtos.Auths;
using Lottery.Dtos.Sells;
using Lottery.Infrastructure.Enums;

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
            return _sellAppService.GetGoodsInfos(_lotterySession.MemberRank,_lotterySession.SystemTypeId,sellType);
        }

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <returns></returns>
        [Route("userauth")]
        [AllowAnonymous]
        public UserAuthOutput GetMyselfAuthInfo()
        {
            return _sellAppService.GetMyselfAuthInfo(_lotterySession.UserId, _lotterySession.SystemTypeId);
        }

    }
}