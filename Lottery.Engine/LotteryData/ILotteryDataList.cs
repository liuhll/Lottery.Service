using System.Collections;
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.LotteryData
{
    /// <summary>
    /// 开奖历史记录
    /// </summary>
    public interface ILotteryDataList : IDictionary<int,ILotteryNumber>
    {
     
        void AddLotteryData(LotteryDataDto data);

        void RemoveLotteryData(LotteryDataDto data);

        void RemoveLotteryData(int period);

        ICollection<int> LotteryDatas(int position, NumberType numberType = NumberType.Number);

        ICollection<int> LotteryDatas(int position, int step);

        ICollection<int> LotteryDatas(NumberType numberType = NumberType.Number,params int[] position);

        ICollection<int> LotteryDatas(int step,params int[] position);


    }
}