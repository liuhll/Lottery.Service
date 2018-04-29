using Lottery.AppService.Account;
using Lottery.Infrastructure.RunTime.Session;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.AppService.Authorize
{
    public abstract class BasePowerChecker : IPowerChecker
    {
        protected readonly ILotterySession _lotterySession;
        protected readonly IUserManager _userManager;

        protected BasePowerChecker(IUserManager userManager)
        {
            _userManager = userManager;
            _lotterySession = NullLotterySession.Instance;
        }

        public virtual async Task<bool> IsGrantedAsync(string permissionName)
        {
            return !string.IsNullOrEmpty(_lotterySession.UserId) && await _userManager.IsGrantedAsync(_lotterySession.UserId, permissionName);
        }

        public virtual async Task<bool> IsGrantedAsync(string userId, string permissionName)
        {
            return await _userManager.IsGrantedAsync(userId, permissionName);
        }

        public virtual async Task<bool> IsGrantedAsync(string urlPath, HttpMethod method)
        {
            return !string.IsNullOrEmpty(_lotterySession.UserId) && await _userManager.IsGrantedAsync(_lotterySession.UserId, urlPath, method.ToString());
        }

        public virtual Task<bool> IsGrantedAsync(string userId, string urlPath, HttpMethod method)
        {
            return _userManager.IsGrantedAsync(userId, urlPath, method.ToString());
        }
    }
}