using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.RoleDto
{
    public class RoleDto
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public SystemType SystemType { get; set; }

        public string Description { get; set; }
    }
}