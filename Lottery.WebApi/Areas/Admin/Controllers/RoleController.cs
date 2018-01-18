using System.Web.Http;
using ENode.Commanding;
using Lottery.WebApi.Controllers.v1;

namespace Lottery.WebApi.Areas.Admin.Controllers
{
    [RoutePrefix("v1/admin")]
    public class RoleController : BaseApiV1Controller
    {
        public RoleController(ICommandService commandService) : base(commandService)
        {
        }

        [Route("role")]
        public string Create()
        {
            return "";
        }
    }
}
