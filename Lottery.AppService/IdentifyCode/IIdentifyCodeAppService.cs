using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.IdentifyCode
{
    public interface IIdentifyCodeAppService
    {
        IdentifyCodeValidOutput GenerateIdentifyCode(string account, AccountRegistType accountType);
    }
}