using ENode.Commanding;

namespace Lottery.WebApi.Controllers.v1
{
    public class PayController : BaseApiV1Controller
    {
        public PayController(ICommandService commandService) : base(commandService)
        {
        }
    }
}