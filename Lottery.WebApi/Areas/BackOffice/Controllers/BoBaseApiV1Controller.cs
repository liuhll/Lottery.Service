using ENode.Commanding;
using Lottery.AppService.Authorize;
using Lottery.Infrastructure;
using Lottery.WebApi.Controllers.v1;

namespace Lottery.WebApi.Areas.BackOffice.Controllers
{
    [SystemTypeAuthorize(LotteryConstants.BackOfficeKey)]
    public abstract class BoBaseApiV1Controller : BaseApiV1Controller
    {
        public BoBaseApiV1Controller(ICommandService commandService) : base(commandService)
        {
        }
    }
}