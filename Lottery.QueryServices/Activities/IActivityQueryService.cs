using Lottery.Dtos.Activity;
using Lottery.Infrastructure.Enums;

namespace Lottery.QueryServices.Activities
{
    public interface IActivityQueryService
    {
        ActivityDto GetAuthAcivity(string authRankId, SellType sellType);
    }
}