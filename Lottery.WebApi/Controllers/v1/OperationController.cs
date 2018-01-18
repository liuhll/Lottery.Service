using System.Web.Http;
using ENode.Commanding;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/operation")]
    public class OperationController : BaseApiV1Controller
    {
        public OperationController(ICommandService commandService) : base(commandService)
        {
        }

        [Route("activity")]
        public string Create()
        {
            return "";
        }
    }
}