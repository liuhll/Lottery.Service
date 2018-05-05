using Lottery.Dtos.CustomService;

namespace Lottery.QueryServices.CustomService
{
    public interface ICustomServiceQueryService
    {
        CustomServiceOutput GetCustomService(string lotteryId);
    }
}