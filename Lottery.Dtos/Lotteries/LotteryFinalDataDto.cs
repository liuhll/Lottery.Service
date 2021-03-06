﻿using System;

namespace Lottery.Dtos.Lotteries
{
    public class LotteryFinalDataDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public int FinalPeriod { get; set; }

        /// <summary>
        /// 今日开奖的第一期
        /// </summary>
        public int TodayFirstPeriod { get; set; }

        /// <summary>
        /// 计划追号状态
        /// </summary>
        public int PlanState { get; set; }

        /// <summary>
        /// 开奖数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime LotteryTime { get; set; }
    }
}