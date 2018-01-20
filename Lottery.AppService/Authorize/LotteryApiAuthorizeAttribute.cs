using System;

namespace Lottery.AppService.Authorize
{
    public class LotteryApiAuthorizeAttribute : Attribute, ILotteryApiAuthorizeAttribute
    {
        /// <summary>
        /// A list of permissions to authorize.
        /// </summary>
        public string[] Permissions { get; }
   
        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// Default: false.
        /// </summary>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="LotteryApiAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public LotteryApiAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
    }
}