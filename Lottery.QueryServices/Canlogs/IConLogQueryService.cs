using Lottery.Dtos.ConLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lottery.QueryServices.Canlogs
{
    public interface IConLogQueryService
    {
        Task<int> GetUserLoginCount(string userId, string systemTypeId);

        ICollection<ConLogDto> GetUserConLogs(string userId, string systemTypeId);

        ICollection<ConLogDto> GetUserInvalidConLogs(string userId, string systemTypeId);

        ConLogDto GetUserConLog(string userId, string systemTypeId, int clientNo, DateTime invalidTime);

        ConLogDto GetUserNewestConLog(string userId, string systemTypeId, int clientNo);
    }
}