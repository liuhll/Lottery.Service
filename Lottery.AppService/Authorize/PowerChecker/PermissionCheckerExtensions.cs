using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.AppService.Authorize
{
    /// <summary>
    /// Extension methods for <see cref="IPowerChecker"/>
    /// </summary>
    public static class PowerCheckerExtensions
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="permissionCode">Name of the permission</param>
        public static bool IsGranted(this IPowerChecker powerChecker, string permissionCode)
        {
            return AsyncHelper.RunSync(() => powerChecker.IsGrantedAsync(permissionCode));
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="userId">User to check</param>
        /// <param name="permissionCode">Name of the permission</param>
        public static bool IsGranted(this IPowerChecker powerChecker, string userId, string permissionCode)
        {
            return AsyncHelper.RunSync(() => powerChecker.IsGrantedAsync(userId, permissionCode));
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="userId">User</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionCodes">Name of the permissions</param>
        public static bool IsGranted(this IPowerChecker powerChecker, string userId, bool requiresAll, params string[] permissionCodes)
        {
            return AsyncHelper.RunSync(() => IsGrantedAsync(powerChecker, userId, requiresAll, permissionCodes));
        }

        /// <summary>
        /// Checks if given user is granted for given permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="userId">User</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionCodes">Name of the permissions</param>
        public static async Task<bool> IsGrantedAsync(this IPowerChecker powerChecker, string userId, bool requiresAll, params string[] permissionCodes)
        {
            if (permissionCodes.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionCode in permissionCodes)
                {
                    if (!(await powerChecker.IsGrantedAsync(userId, permissionCode)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionCode in permissionCodes)
                {
                    if (await powerChecker.IsGrantedAsync(userId, permissionCode))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if current user is granted for given permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionCodes">Name of the permissions</param>
        public static bool IsGranted(this IPowerChecker powerChecker, bool requiresAll, params string[] permissionCodes)
        {
            return AsyncHelper.RunSync(() => IsGrantedAsync(powerChecker, requiresAll, permissionCodes));
        }

        /// <summary>
        /// Checks if current user is granted for given permission.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionCodes">Name of the permissions</param>
        public static async Task<bool> IsGrantedAsync(this IPowerChecker powerChecker, bool requiresAll, params string[] permissionCodes)
        {
            if (permissionCodes.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionCode in permissionCodes)
                {
                    if (!(await powerChecker.IsGrantedAsync(permissionCode)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionCode in permissionCodes)
                {
                    if (await powerChecker.IsGrantedAsync(permissionCode))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="LotteryAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionCodes"/> are granted.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="permissionCodes">Name of the permissions to authorize</param>
        /// <exception cref="LotteryAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPowerChecker powerChecker, params string[] permissionCodes)
        {
            Authorize(powerChecker, false, permissionCodes);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="LotteryAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionCodes"/> are granted.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionCodes"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionCodes"/> must be granted.
        /// </param>
        /// <param name="permissionCodes">Name of the permissions to authorize</param>
        /// <exception cref="LotteryAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPowerChecker powerChecker, bool requireAll, params string[] permissionCodes)
        {
            AsyncHelper.RunSync(() => AuthorizeAsync(powerChecker, requireAll, permissionCodes));
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="LotteryAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionCodes"/> are granted.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="permissionCodes">Name of the permissions to authorize</param>
        /// <exception cref="LotteryAuthorizationException">Throws authorization exception if</exception>
        public static Task AuthorizeAsync(this IPowerChecker powerChecker, params string[] permissionCodes)
        {
            return AuthorizeAsync(powerChecker, false, permissionCodes);
        }

        public static async Task AuthorizeAsync(this IPowerChecker powerChecker, string userId, string absolutePath,
            HttpMethod method)
        {
            await powerChecker.IsGrantedAsync(userId, absolutePath, method);
        }

        public static async Task AuthorizeAsync(this IPowerChecker powerChecker, string absolutePath,
            HttpMethod method)
        {
            await powerChecker.IsGrantedAsync(absolutePath, method);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="LotteryAuthorizationException"/> if not authorized.
        /// </summary>
        /// <param name="powerChecker">Permission checker</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionCodes"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionCodes"/> must be granted.
        /// </param>
        /// <param name="permissionCodes">Name of the permissions to authorize</param>
        /// <exception cref="LotteryAuthorizationException">Throws authorization exception if</exception>
        public static async Task AuthorizeAsync(this IPowerChecker powerChecker, bool requireAll, params string[] permissionCodes)
        {
            if (await IsGrantedAsync(powerChecker, requireAll, permissionCodes))
            {
                return;
            }

            var permissionNameStrs = permissionCodes.ToSplitString();

            if (requireAll)
            {
                throw new LotteryAuthorizeException($"需要被授予所有的{permissionNameStrs}权限");
            }
            else
            {
                throw new LotteryAuthorizeException($"需要被授予至少一个的{permissionNameStrs}权限");
            }
        }
    }
}