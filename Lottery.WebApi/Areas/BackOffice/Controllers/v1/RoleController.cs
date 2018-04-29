using ENode.Commanding;
using System.Web.Http;

namespace Lottery.WebApi.Areas.BackOffice.Controllers.v1
{
    [RoutePrefix("api/v1/backoffice")]
    public class RoleController : BoBaseApiV1Controller
    {
        public RoleController(ICommandService commandService) : base(commandService)
        {
        }

        [Route("role")]
        [HttpPost]
        public string CreateRole()
        {
            return "";
        }
    }
}