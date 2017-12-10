using Lottery.Dtos.Lotteries;
using Lottery.Engine.Predictor;
using Lottery.Engine.TimeRule;

namespace Lottery.Engine
{
    public interface ILotterEngine
    {

        LotteryInfoDto LotteryInfo { get; }

        ITimeRuleManager TimeRuleManager { get; }

        IPerdictor GetPerdictor(string predictCode);

    }
}
