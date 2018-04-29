using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum MemberRank
    {
        /// <summary>
        /// 普通版本
        /// </summary>
        [EnumDescribe("普通版")]
        Ordinary = 1,

        /// <summary>
        /// 高级版本
        /// </summary>
        [EnumDescribe("高级版")]
        Senior,

        /// <summary>
        /// 专业版本
        /// </summary>
        [EnumDescribe("专业版")]
        Specialty,

        /// <summary>
        /// 精英版
        /// </summary>
        [EnumDescribe("精英版")]
        Elite,

        /// <summary>
        /// 测试版
        /// </summary>
        [EnumDescribe("测试版")]
        Testing
    }
}