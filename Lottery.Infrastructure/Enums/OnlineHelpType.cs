using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum OnlineHelpType
    {
        /// <summary>
        /// 产品帮助
        /// </summary>
        [EnumDescribe("产品帮助")]
        Product = 1,

        [EnumDescribe("积分帮助")]
        Point = 2,

        [EnumDescribe("彩种玩法")]
        Lottery = 3

    }
}