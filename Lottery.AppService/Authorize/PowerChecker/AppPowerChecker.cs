using ECommon.Components;
using Lottery.AppService.Account;
using Lottery.AppService.Member;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.AppService.Authorize
{
    [Component]
    public class AppPowerChecker : BasePowerChecker
    {
        private readonly IMermberManager _mermberManager;

        public AppPowerChecker(IUserManager userManager,
            IMermberManager mermberManager) : base(userManager)
        {
            _mermberManager = mermberManager;
        }

        public override async Task<bool> IsGrantedAsync(string permissionName)
        {
            return !string.IsNullOrEmpty(_lotterySession.UserId)
                && _lotterySession.SystemType == SystemType.App
                && !string.IsNullOrEmpty(_lotterySession.SystemTypeId)
                && await _mermberManager.IsGrantedAsync(_lotterySession.UserId, _lotterySession.SystemTypeId, permissionName);
        }

        public override async Task<bool> IsGrantedAsync(string userId, string permissionName)
        {
            throw new LotteryAuthorizationException("无法判其他用户的授权信息,只允许用户自己获取授权信息");
        }

        public override async Task<bool> IsGrantedAsync(string urlPath, HttpMethod method)
        {
            return !string.IsNullOrEmpty(_lotterySession.UserId)
                   && _lotterySession.SystemType == SystemType.App
                   && !string.IsNullOrEmpty(_lotterySession.SystemTypeId)
                   && await _mermberManager.IsGrantedAsync(_lotterySession.UserId, _lotterySession.SystemTypeId, urlPath, method);
        }

        public override async Task<bool> IsGrantedAsync(string userId, string urlPath, HttpMethod method)
        {
            throw new LotteryAuthorizationException("无法判其他用户的授权信息,只允许用户自己获取授权信息");
        }
    }
}