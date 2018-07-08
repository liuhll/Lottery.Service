using System;
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public interface IJudgePredictDataResult
    {
        PredictedResult JudgePredictDataResult(LotteryInfoDto lotteryInfo, PredictDataDto startPeriodData, NormConfigDto userNormConfig);

    }
}
