using System.Collections.Generic;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Power;
using Lottery.Dtos.Menus;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/menu")]
    public class MenuController : BaseApiV1Controller
    {
        private readonly IPowerManager _powerManager;

        public MenuController(ICommandService commandService, 
            IPowerManager powerManager) 
            : base(commandService)
        {
            _powerManager = powerManager;
        }

        /// <summary>
        /// 获取登录用户的权限菜单(前端路由)
        /// </summary>
        /// <returns></returns>
        [Route("route")]
        [HttpGet]
        [AllowAnonymous]
        public ICollection<RouteDto> GetFrontEndRoute()
        {
            return _powerManager.GetUserRoutes(_lotterySession.UserId, _lotterySession.SystemType);
        }
    }
}