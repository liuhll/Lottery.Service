using Lottery.Dtos.Account;
using System.Collections.Generic;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserClientTypeQueryService
    {
        ICollection<UserSystemTypeDto> GetUserSystemTypes(string userId);
    }
}