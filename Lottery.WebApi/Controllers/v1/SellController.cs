using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Sell;
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

        [Route("salestype")]
        [AllowAnonymous]
        public ICollection<SellTypeOutput> GetSalesType()
        {

            return _sellAppService.GetSalesType(_lotterySession.MemberRank);
        }


        [Route("goodslist")]
        [AllowAnonymous]
        public ICollection<GoodsOutput> GetGoodInfos(SellType sellType)
        {
            return _sellAppService.GetGoodsInfos(_lotterySession.MemberRank,_lotterySession.SystemTypeId,sellType);
        }


    }
}