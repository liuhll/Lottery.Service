using Lottery.Dtos.Norms;

namespace Lottery.QueryServices.Norms
{
    public interface IUserNormDefaultConfigService
    {
        UserNormDefaultConfigDto GetUserNormDefaultConfig(string userId, string lotteryId);
    }
}