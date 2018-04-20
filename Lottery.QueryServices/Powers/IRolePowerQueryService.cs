using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.QueryServices.Powers
{
    public interface IRolePowerQueryService
    {
        ICollection<PowerGrantInfo> GetPermissions(string roleId);
    }
}