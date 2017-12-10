using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;

namespace Lottery.Engine.Services.LotteryData
{
    public interface ILotteryDataService
    {
        // ICollection<LotteryDataDto> AllDatas(string lotteryId);

        ILotteryDataList LotteryDataList(string lotteryId);

        IList<PredictDataDto> GetLotteryDataList(int peroid, NormConfigDto norm);
    }
}