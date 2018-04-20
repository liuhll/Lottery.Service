using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.QueryServices.Powers
{
    public interface IUserPowerQueryService
    {
        ICollection<PowerGrantInfo> GetPermissions(string userId);
    }
}