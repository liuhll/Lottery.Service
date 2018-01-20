using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Lottery.AppService.Authorize
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<ILotteryApiAuthorizeAttribute> authorizeAttributes);
        Task AuthorizeAsync(string absolutePath, HttpMethod method);

    }
}