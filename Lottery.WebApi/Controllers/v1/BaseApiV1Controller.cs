using System.Web.Http;
using ENode.Commanding;

namespace Lottery.WebApi.Controllers.v1
{
    public abstract class BaseApiV1Controller : BaseApiController
    {
        public BaseApiV1Controller(ICommandService commandService) : base(commandService)
        {
        }
    }
}
