using ENode.Commanding;

namespace Lottery.WebApi.Controllers.v1
{
    public class PointController : BaseApiV1Controller
    {
        public PointController(ICommandService commandService) : base(commandService)
        {
        }
    }
}
