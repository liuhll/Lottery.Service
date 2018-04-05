using Lottery.Dtos.Lotteries;
using Lottery.Engine.Predictor;
using Lottery.Engine.TimeRule;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine
{
    public interface ILotterEngine
    {

        LotteryInfoDto LotteryInfo { get; }

        ITimeRuleManager TimeRuleManager { get; }

        IPerdictor GetPerdictor(AlgorithmType algorithmType);

    }
}
