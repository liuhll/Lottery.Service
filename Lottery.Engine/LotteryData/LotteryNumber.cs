﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.LotteryData
{
    public class LotteryNumber : ILotteryNumber
    {
        private readonly LotteryDataDto _lotteryData;
        private readonly int[] _datas;

        public LotteryNumber(LotteryDataDto lotteryData)
        {
            _lotteryData = lotteryData;
            _datas = _lotteryData.Data.Split(',').Select(p => Convert.ToInt32(p)).ToArray();
        }

        public LotteryDataDto LotteryData {
            get { return _lotteryData; }
        }

        public int this[int position]
        {
            get { return _datas[position - 1]; }
        }

        public int[] Datas {
            get { return _datas; }
        }

        public int Period {
            get { return _lotteryData.Period; }
        }
    }
}