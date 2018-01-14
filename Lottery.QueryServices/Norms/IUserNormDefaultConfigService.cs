using Lottery.Dtos.Norms;

namespace Lottery.QueryServices.Norms
{
    public interface IUserNormDefaultConfigService
    {
        UserNormDefaultConfigOutput GetUserNormDefaultConfig(string userId, string lotteryId);
    }
}