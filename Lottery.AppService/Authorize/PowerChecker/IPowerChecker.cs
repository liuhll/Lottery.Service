using System.Net.Http;
using System.Threading.Tasks;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Authorize
{
    public interface IPowerChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(string userId, string permissionName);

        /// <summary>
        /// 通过RUL绝对地址和Http谓词判断请求的用户是否有访问某个API的权限
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(string urlPath, HttpMethod method);

        /// <summary>
        /// 通过RUL绝对地址和Http谓词判断某个用户是否有访问某个API的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="urlPath"></param>
        /// <param name="method"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(string userId, string urlPath,HttpMethod method);
       
    }
}