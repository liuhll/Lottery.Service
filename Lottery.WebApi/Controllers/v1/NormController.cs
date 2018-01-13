using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Norm;
using Lottery.Dtos.Norms;

namespace Lottery.WebApi.Controllers.v1
{
    /// <summary>
    /// 追号计划指标控制器
    /// </summary>
    [RoutePrefix("v1/norm")]
    public class NormController : BaseApiV1Controller
    {
        private readonly INormConfigAppService _normConfigAppService;

        public NormController(ICommandService commandService, 
            INormConfigAppService normConfigAppService) 
            : base(commandService)
        {
            _normConfigAppService = normConfigAppService;
        }

        /// <summary>
        /// 用户默认的公式指标
        /// </summary>
        /// <param name="lotteryId">彩种Id</param>
        /// <returns></returns>
        [Route("usernormdefaultconfig")]
        [HttpGet]
        public UserNormDefaultConfigDto GetUserNormDefaultConfig(string lotteryId)
        {
            return _normConfigAppService.GetUserNormDefaultConfig(_lotterySession.UserId, lotteryId);
        }
    }
}
