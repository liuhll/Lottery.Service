using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum PurchaseType
    {
        /// <summary>
        /// 新购
        /// </summary>
        [EnumDescribe("新购")]
        New = 0,

        /// <summary>
        /// 续费
        /// </summary>
        [EnumDescribe("续费")]
        Continuation,

        /// <summary>
        /// 升级
        /// </summary>
        [EnumDescribe("升级")]
        Upgrade
    }
}