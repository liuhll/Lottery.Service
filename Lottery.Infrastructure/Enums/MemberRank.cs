﻿using Lottery.Infrastructure.Attributes;

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
        /// 团队版本
        /// </summary>
        [EnumDescribe("团队版")]
        Team,
    }
}