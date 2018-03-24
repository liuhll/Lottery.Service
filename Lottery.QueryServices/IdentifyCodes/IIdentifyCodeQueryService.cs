using Lottery.Dtos.IdentifyCodes;

namespace Lottery.QueryServices.IdentifyCodes
{
    public interface IIdentifyCodeQueryService
    {
        IdentifyCodeDto GetIdentifyCode(string receiver);
    }
}