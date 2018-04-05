using System.Collections.Generic;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Lotteries
{
    public class PlanInfoDto
    {
        public string Id { get; set; }

        public string PlanCode { get; set; }

        public string PlanNormTable { get; set; }

        public string PlanName { get; set; }

        public string PredictCode { get; set; }

        public AlgorithmType AlgorithmType { get; set; }
        //public string LotteryId { get; set; }

        public PredictType DsType { get; set; }

        public LotteryInfoDto LotteryInfo { get; set; }

        public IList<PositionInfoDto> PositionInfos { get; set; }

        public PlanPosition PlanPosition
        {
            get
            {
                if (PositionInfos == null)
                {
                    return PlanPosition.NoPostion;
                }
                if (PositionInfos.Count == 1)
                {
                    return PlanPosition.Single;
                }

                return PlanPosition.Multiple;
            }
        }

    }
}