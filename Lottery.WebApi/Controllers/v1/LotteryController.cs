using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.Dtos.Lotteries;
using Lottery.WebApi.RunTime.Session;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/lottery")]
    public class LotteryController : BaseApiV1Controller
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly ILotterySession _lotterySession;

        public LotteryController(ILotteryDataAppService lotteryDataAppService,ICommandService commandService) 
            : base(commandService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _lotterySession = NullLotterySession.Instance;
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
    }
}
