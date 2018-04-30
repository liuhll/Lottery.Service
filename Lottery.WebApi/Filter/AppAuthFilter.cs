using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ECommon.Components;
using Lottery.AppService.Operations;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;

namespace Lottery.WebApi.Filter
{
    public class AppAuthFilter : ActionFilterAttribute
    {
        private readonly string _desc;
        private readonly ILotterySession _lotterySession;
        private readonly IMemberAppService _memberAppService;


        public AppAuthFilter(string desc)
        {
            _desc = desc;
            _lotterySession = NullLotterySession.Instance;
            _memberAppService = ObjectContainer.Resolve<IMemberAppService>();
          
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var userMemberRank = _memberAppService.GetUserMemRank(_lotterySession.UserId, _lotterySession.SystemTypeId);
            if (userMemberRank == MemberRank.Ordinary)
            {
                throw new LotteryException(_desc);
            }
        }
    }
}