using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Dtos.Power;
using Lottery.Dtos.RoleDto;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Role
{
    public interface IRoleManager
    {
        Task<bool> IsGrantedAsync(string roleId, string powerCode);

        Task<bool> IsGrantedAsync(string roleId, PowerDto power);

        ICollection<RoleDto> GetUserRoles(string userId);
    }
}