using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Plan;
using Lottery.Dtos.Plans;

namespace Lottery.WebApi.Controllers.v1
{
    /// <summary>
    /// 彩票计划相关控制器
    /// </summary>
    [RoutePrefix("v1/plan")]
    public class PlanController : BaseApiV1Controller
    {
        private readonly IPlanInfoAppService _planInfoAppService;


        public PlanController(ICommandService commandService, 
            IPlanInfoAppService planInfoAppService) : base(commandService)
        {
            _planInfoAppService = planInfoAppService;
        }

        /// <summary>
        /// 获取用户计划接口
        /// </summary>
        /// <remarks>获取用户设置的计划类型,如果用户未设置计划，则获取系统默认的计划</remarks>
        /// <param name="lotteryId">彩种Id</param>
        /// <returns>用户设置的计划信息</returns>
        [HttpGet]
        [Route("userplans")]
        public UserPlanInfoDto GetUserPlans(string lotteryId)
        {
            return _planInfoAppService.GetUserPlanInfo(lotteryId, _lotterySession.UserId);
        }
    }
}