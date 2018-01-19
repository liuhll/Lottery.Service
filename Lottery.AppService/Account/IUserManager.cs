using System.Threading.Tasks;
using Lottery.Dtos.Account;

namespace Lottery.AppService.Account
{
    public interface IUserManager
    {
        Task<UserInfoViewModel> SignInAsync(string userName, string password);

        Task<UserTicketDto> GetValidTicketInfo(string userId);

        Task<bool> IsExistAccount(string userAccount);

        void VerifyUserSystemType(string userId, string systemId);
    }
}