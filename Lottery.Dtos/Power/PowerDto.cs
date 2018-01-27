using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Power
{
    public class PowerDto
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string PowerCode { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PowerName { get; set; }

        /// <summary>
        /// 父权限ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// URI
        /// </summary>
        public string ApiPath { get; set; }

        public string HttpMethod { get; set; }

        public SystemType SystemType { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string PowerType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}