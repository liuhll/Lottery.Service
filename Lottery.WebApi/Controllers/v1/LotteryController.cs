using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.PageList;
using Lottery.WebApi.RunTime.Session;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/lottery")]
    public class LotteryController : BaseApiV1Controller
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;

        public LotteryController(ILotteryDataAppService lotteryDataAppService,ICommandService commandService) 
            : base(commandService)
        {
            _lotteryDataAppService = lotteryDataAppService;
        }

        /// <summary>
        /// 计划追号接口
        /// </summary>
        /// <param name="lotteryId">彩种Id</param>
        /// <param name="predictPeriod">预测的期号</param>
        /// <returns>返回计划追号结果</returns>
        [HttpGet]
        [Route("predictdatas")]
        [SwaggerOptionalParameter("predictPeriod")]
        public ICollection<PredictDataDto> GetPredictDatas(string lotteryId,int? predictPeriod = null)
        {
            var userInfo = _lotterySession.UserId;
            var data = _lotteryDataAppService.NewLotteryDataList(lotteryId, predictPeriod, "");
            return data;
        }

        /// <summary>
        /// 获取历史开奖数据
        /// </summary>
        /// <param name="lotteryId">彩种Id</param>
        /// <param name="pageIndex">分页数</param>
        /// <returns>开奖数据</returns>
        [HttpGet]
        [Route("list")]
        public IPageList<LotteryDataDto> List(string lotteryId,int pageIndex = 1)
        {
            var list = _lotteryDataAppService.GetList(lotteryId);
            return new PageList<LotteryDataDto>(list,pageIndex);
        }
    }
}
