using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Power
{
    public class PowerDto
    {
        public string Id { get; set; }

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

        public string Name { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public bool Hidden { get; set; }

        public string Redirect { get; set; }

        public string Meta { get; set; }

        public SystemType SystemType { get; set; }

        /// <summary>
        /// 权限类型 0 -- 公共的   1-- 系统的
        /// </summary>
        public int PowerType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}