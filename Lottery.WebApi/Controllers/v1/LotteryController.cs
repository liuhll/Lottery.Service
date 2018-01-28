using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.PageList;

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
        /// <param name="predictPeriod">预测的期号</param>
        /// <returns>返回计划追号结果</returns>
        [HttpGet]
        [Route("predictdatas")]
        [SwaggerOptionalParameter("predictPeriod")]
        public ICollection<PlanTrackNumber> GetPredictDatas(int? predictPeriod = null)
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var userId = _lotterySession.UserId;
            var data = _lotteryDataAppService.NewLotteryDataList(lotteryId, predictPeriod, userId);
            var predictResults = _lotteryDataAppService.GetpredictResultData(data,lotteryId); 
            return predictResults;
        }

        /// <summary>
        /// 获取历史开奖数据
        /// </summary>     
        /// <param name="pageIndex">分页数</param>
        /// <returns>开奖数据</returns>
        [HttpGet]
        [Route("history")]
        public IPageList<LotteryDataDto> List(int pageIndex = 1)
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var list = _lotteryDataAppService.GetList(lotteryId);
            return new PageList<LotteryDataDto>(list,pageIndex);
        }

        /// <summary>
        /// 获取最后一期开奖数据
        /// </summary>
        /// <returns>最后一期开奖数据</returns>
        [HttpGet]
        [Route("finallotterydata")]
        public FinalLotteryDataOutput GetFinalLotteryData()
        {
            var lotteryId = _lotterySession.SystemTypeId;
            return _lotteryDataAppService.GetFinalLotteryData(lotteryId);
        }
    }
}
