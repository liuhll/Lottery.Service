using System;
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryDataQueryService
    {
        ICollection<LotteryDataDto> GetAllDatas(string lotteryId, int count = 10000);

        ICollection<LotteryDataDto> GetPredictPeriodDatas(string lotteryId, int predictPeriod, int userNormHistoryCount);

        LotteryDataDto GetPredictPeriodData(string lotteryId, int period);
        ICollection<LotteryDataDto> GetLotteryDatas(string lotteryId, DateTime lotteryTime);
    }
}