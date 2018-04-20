using Lottery.Dtos.Norms;

namespace Lottery.QueryServices.Norms
{
    public interface IUserNormDefaultConfigService
    {
        UserNormDefaultConfigOutput GetUserNormOrDefaultConfig(string userId, string lotteryId);

        UserNormDefaultConfigDto GetUserNormConfig(string userId, string lotteryId);
    }
}