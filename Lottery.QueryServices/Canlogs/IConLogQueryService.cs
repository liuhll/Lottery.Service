using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Dtos.ConLog;

namespace Lottery.QueryServices.Canlogs
{
    public interface IConLogQueryService
    {
        Task<int> GetUserLoginCount(string userId, string systemTypeId);
        ICollection<ConLogDto> GetUserConLogs(string userId, string systemTypeId);

        ICollection<ConLogDto> GetUserInvalidConLogs(string userId, string systemTypeId);
    }
}