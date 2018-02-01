using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;

namespace Lottery.AppService.LotteryData
{
    public interface ILotteryDataAppService
    {
        ILotteryDataList LotteryDataList(string lotteryId);

        /// <summary>
        /// 获取新一期的预测数据
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<PredictDataDto> NewLotteryDataList(string lotteryId, string userId);

        ICollection<LotteryDataDto> GetList(string lotteryId);

        FinalLotteryDataOutput GetFinalLotteryData(string lotteryId);

        LotteryDataDto GetLotteryData(string lotteryInfoId, int currentPredictPeriod);
    }
}