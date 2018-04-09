using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum PointOperationType
    {
        /// <summary>
        /// 增加积分
        /// </summary>
        [EnumDescribe("增")]
        Increase = 0,

        /// <summary>
        /// 消费积分
        /// </summary>
        [EnumDescribe("减")]
        Consume = 1
    }
}