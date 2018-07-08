using ENode.Commanding;
using System.Web.Http;

namespace Lottery.WebApi.Controllers.v1
{
    [AllowAnonymous]
    public abstract class BaseApiV1Controller : BaseApiController
    {
        public BaseApiV1Controller(ICommandService commandService) : base(commandService)
        {
        }
    }
}