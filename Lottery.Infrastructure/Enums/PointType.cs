using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum PointType
    {
        [EnumDescribe("注册")]
        Register = 1,

        [EnumDescribe("签到")]
        Signed = 2,

        [EnumDescribe("连续签到")]
        SignAdditional = 3,

        [EnumDescribe("分享App")]
        ShareApp = 4

    }
}