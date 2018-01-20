using System.Collections.Generic;
using Lottery.Dtos.RoleDto;

namespace Lottery.QueryServices.Roles
{
    public interface IRoleQueryService
    {
        ICollection<RoleDto> GetUserRoles(string userId);
    }
}