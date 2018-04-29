using ENode.Domain;

namespace Lottery.Core.Domain.PositionInfos
{
    public class PositionInfo : AggregateRoot<string>
    {
        public PositionInfo(
          string id,
          string lotteryId,
          string name,
          string positionType,
          int position,
          int maxValue,
          int minxValue
          ) : base(id)
        {
            LotteryId = lotteryId;
            Name = name;
            PositionType = positionType;
            Position = position;
            MaxValue = maxValue;
            MinxValue = minxValue;
        }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 位置名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 位置类型
        /// </summary>
        public string PositionType { get; private set; }

        /// <summary>
        /// 位置编码
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// 允许的最大值
        /// </summary>
        public int MaxValue { get; private set; }

        /// <summary>
        /// 允许的最小值
        /// </summary>
        public int MinxValue { get; private set; }
    }
}