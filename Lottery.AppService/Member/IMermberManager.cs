using Lottery.Infrastructure.Enums;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.AppService.Member
{
    public interface IMermberManager
    {
        Task<bool> IsGrantedAsync(string userId, string lotteryId, string powerCode);

        Task<bool> IsGrantedAsync(string userId, string lotteryId, string urlPath, HttpMethod method);

        MemberRank GetUserMemberRank(string userId, string lotteryId);
    }
}