using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.IdentifyCode
{
    public interface IIdentifyCodeAppService
    {
        IdentifyCodeOutput GenerateIdentifyCode(string account, AccountRegistType accountType);
        IdentifyCodeValidOutput ValidIdentifyCode(string account, string identifyCode);
    }
}