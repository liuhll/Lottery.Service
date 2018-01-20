using System.Net.Http;
using System.Threading.Tasks;
using ECommon.Components;
using Lottery.AppService.Account;

namespace Lottery.AppService.Authorize
{
    [Component]
    public class OfficialWebsitePowerChecker : BasePowerChecker
    {
        public OfficialWebsitePowerChecker(IUserManager userManager) : base(userManager)
        {
        }

        public override async Task<bool> IsGrantedAsync(string permissionName)
        {
            return await Task.FromResult(true);
        }

        public override async Task<bool> IsGrantedAsync(string userId, string permissionName)
        {
            return await Task.FromResult(true);
        }

        public override async Task<bool> IsGrantedAsync(string urlPath, HttpMethod method)
        {
            return await Task.FromResult(true);
        }

        public override async Task<bool> IsGrantedAsync(string userId, string urlPath, HttpMethod method)
        {
            return await Task.FromResult(true);
        }

     
    }
}