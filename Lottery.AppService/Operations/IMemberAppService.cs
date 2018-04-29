using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Operations
{
    public interface IMemberAppService
    {
        string ConcludeUserMemRank(string userId, string systemTypeId);

        MemberRank GetUserMemRank(string userId, string systemTypeId);
    }
}