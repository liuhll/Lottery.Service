using Lottery.Dtos.Account;
using System.Threading.Tasks;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserTicketService
    {
        Task<UserTicketDto> GetValidTicketInfo(string userId);
    }
}