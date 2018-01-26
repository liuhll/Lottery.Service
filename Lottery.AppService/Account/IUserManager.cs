using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Core.Domain.LogonLog;
using Lottery.Dtos.Account;
using Lottery.Dtos.ConLog;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Account
{
    public interface IUserManager
    {
        Task<UserInfoViewModel> SignInAsync(string userName, string password);

        Task<UserTicketDto> GetValidTicketInfo(string userId);

        Task<bool> IsExistAccount(string userAccount);

        void VerifyUserSystemType(string userId, string systemType);

        Task<bool> IsGrantedAsync(string userId, string powerCode);

        Task<bool> IsGrantedAsync(string userId, string urlPath, string method);

        string CreateToken(UserInfoViewModel userInfo, string systemTypeId,int clientNo, out DateTime invalidDateTime);

        string UpdateToken(string userId,string systemTypeId, int clientNo, out DateTime invalidDateTime);

        Task<int> VerifyUserClientNo(string userId, string systemTypeId);
    }
}