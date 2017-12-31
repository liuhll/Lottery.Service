using System.Threading.Tasks;
using Lottery.Dtos.Account;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserTicketService
    {
        Task<UserTicketDto> GetValidTicketInfo(string userId);
    }
}