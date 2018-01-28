using System.Collections.Generic;
using Lottery.Dtos.Power;

namespace Lottery.QueryServices.Powers
{
    public interface IUserPowerQueryService
    {
        ICollection<PowerGrantInfo> GetPermissions(string userId);
      
    }
}