using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Enums
{
    public enum SystemType
    {
        [EnumDescribe("移动客户端")]
        App = 0,

        [EnumDescribe("后台管理系统")]
        BackOffice,

        [EnumDescribe("门户网站")]
        OfficialWebsite,

    }
}