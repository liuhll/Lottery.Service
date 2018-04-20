using ENode.Domain;

namespace Lottery.Core.Domain.NormGroups
{
    public class NormGroup : AggregateRoot<string>
    {
        public NormGroup(
          string id,
          string lotteryId,
          string groupCode,
          string groupName,
          int? sort
          ) : base(id)
        {
            LotteryId = lotteryId;
            GroupCode = groupCode;
            GroupName = groupName;
            Sort = sort;
        }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 计划分组编码
        /// </summary>
        public string GroupCode { get; private set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int? Sort { get; private set; }
    }
}