using System.Threading.Tasks;
using Lottery.Dtos.Account;

namespace Lottery.AppService.Account
{
    public interface IUserManager
    {
        Task<UserInfoViewModel> SignInAsync(string userName, string password);

        Task<UserTicketDto> GetValidTiectInfo(string userId);
    }
}