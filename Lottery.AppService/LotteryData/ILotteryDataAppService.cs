﻿using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 更新预测数据
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<PredictDataDto> UpdateLotteryDataList(string lotteryId, string userId);

        /// <summary>
        /// 更新单个指标预算
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="userId"></param>
        /// <param name="normId"></param>
        /// <returns></returns>
        IList<PredictDataDto> UpdateLotteryDataList(string lotteryId, string userId, string normId);

        ICollection<LotteryDataDto> GetList(string lotteryId, DateTime? lotteryTime);

        FinalLotteryDataOutput GetFinalLotteryData(string lotteryId);

        LotteryDataDto GetLotteryData(string lotteryInfoId, int currentPredictPeriod);
    }
}