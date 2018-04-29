using Lottery.Dtos.RoleDto;
using System.Collections.Generic;

namespace Lottery.QueryServices.Roles
{
    public interface IRoleQueryService
    {
        ICollection<RoleDto> GetUserRoles(string userId);

        ICollection<RoleDto> GetMermberRoles(string lotteryId, int memberRank);
    }
}