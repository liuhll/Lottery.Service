using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;

namespace Lottery.AppService.LotteryData
{
    public interface ILotteryDataAppService
    {
        ILotteryDataList LotteryDataList(string lotteryId);

        /// <summary>
        /// 获取新一期的开奖数据
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="peroid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<PredictDataDto> NewLotteryDataList(string lotteryId, int? peroid, string userId);
    }
}