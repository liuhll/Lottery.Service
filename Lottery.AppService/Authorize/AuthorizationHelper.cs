using ECommon.Components;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.AppService.Authorize
{
    [Component]
    public class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly ILotterySession _lotterySession;
        private readonly IPowerChecker _powerChecker;

        public AuthorizationHelper()
        {
            _lotterySession = NullLotterySession.Instance;
            switch (_lotterySession.SystemType)
            {
                case SystemType.BackOffice:
                    _powerChecker = ObjectContainer.Resolve<BackOfficePowerChecker>();
                    break;

                case SystemType.App:
                    _powerChecker = ObjectContainer.Resolve<AppPowerChecker>();
                    break;

                case SystemType.OfficialWebsite:
                    _powerChecker = ObjectContainer.Resolve<OfficialWebsitePowerChecker>();
                    break;
            }
        }

        public async Task AuthorizeAsync(IEnumerable<ILotteryApiAuthorizeAttribute> authorizeAttributes)
        {
            if (_lotterySession == null || string.IsNullOrEmpty(_lotterySession.UserId))
            {
                throw new LotteryAuthorizationException("您还未登录,请先登录");
            }
            foreach (var authorizeAttribute in authorizeAttributes)
            {
                await _powerChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }

        public async Task AuthorizeAsync(string absolutePath, HttpMethod method)
        {
            if (_lotterySession == null || string.IsNullOrEmpty(_lotterySession.UserId))
            {
                throw new LotteryAuthorizationException("您还未登录,请先登录");
            }
            await _powerChecker.AuthorizeAsync(absolutePath, method);
        }
    }
}