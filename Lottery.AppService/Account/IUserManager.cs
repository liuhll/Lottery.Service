using System;
using System.Threading.Tasks;
using Lottery.Dtos.Account;

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

        Task<bool> VerifyPassword(string account, string password);
    }
}