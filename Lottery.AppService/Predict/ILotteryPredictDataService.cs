using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.AppService.Predict
{
    public interface ILotteryPredictDataService
    {
        ICollection<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm, int predictPeroid, string lotteryCode, bool isSwitchFormula = false);
    }
}