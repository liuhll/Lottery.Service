using System.Collections.Generic;
using Lottery.Dtos.Account;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserClientTypeQueryService
    {
        ICollection<UserSystemTypeDto> GetUserSystemTypes(string userId);
    }
}