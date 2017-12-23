using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.LotteryData
{
    public interface ILotteryNumber
    {
        LotteryDataDto LotteryData { get; }

        int this[int position] { get; }

        int[] Datas { get; }

        int Period { get; }

        int[] GetLotteryNumbers(int[] positions);

        int GetRankNumber(int rank);

    }
}