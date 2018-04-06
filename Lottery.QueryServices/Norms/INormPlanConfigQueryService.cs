using System.Threading.Tasks;
using Lottery.Dtos.Norms;

namespace Lottery.QueryServices.Norms
{
    public interface INormPlanConfigQueryService
    {
        NormPlanConfigDto GetNormPlanDefaultConfig(string lotteryCode,string predictCode = null);
    }
}