using Lottery.Dtos.Power;
using Lottery.Dtos.RoleDto;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lottery.AppService.Role
{
    public interface IRoleManager
    {
        Task<bool> IsGrantedAsync(string roleId, string powerCode);

        Task<bool> IsGrantedAsync(string roleId, PowerDto power);

        ICollection<RoleDto> GetUserRoles(string userId);

        Task<bool> IsGrantedMermberAsync(string roleId, PowerDto power);

        ICollection<RoleDto> GetMermberRoles(string lotteryId, MemberRank memberRank);
    }
}