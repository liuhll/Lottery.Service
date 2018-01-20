using System.Collections.Generic;
using Lottery.Dtos.Power;

namespace Lottery.QueryServices.Powers
{
    public interface IRolePowerQueryService
    {
        ICollection<PowerGrantInfo> GetPermissions(string roleId);
    }
}