using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum SystemType
    {
        /// <summary>
        /// 移动客户端 APP
        /// </summary>
        [EnumDescribe("移动客户端")]
        App = 0,

        /// <summary>
        /// 后台管理系统
        /// </summary>
        [EnumDescribe("后台管理系统")]
        BackOffice,

        /// <summary>
        /// 门户网站
        /// </summary>
        [EnumDescribe("门户网站")]
        OfficialWebsite,
    }
}