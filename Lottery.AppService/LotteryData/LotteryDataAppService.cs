using System;
using System.Collections.Generic;
using ECommon.Components;
using Lottery.AppService.Predict;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.Exceptions;
using Lottery.Engine.LotteryData;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.LotteryData
{
    [Component]
    public class LotteryDataAppService : ILotteryDataAppService
    {
        private readonly ILotteryDataQueryService _lotteryDataQueryService;       
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly ILotteryPredictDataService _lotteryPredictDataService;
        private readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;

        public LotteryDataAppService(
            ILotteryDataQueryService lotteryDataQueryService,
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataService lotteryPredictDataService,
            ILotteryFinalDataQueryService lotteryFinalDataQueryService)
        {
            _lotteryDataQueryService = lotteryDataQueryService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataService = lotteryPredictDataService;
            _lotteryFinalDataQueryService = lotteryFinalDataQueryService;
        }

        public ICollection<LotteryDataDto> AllDatas(string lotteryId)
        {
            return _lotteryDataQueryService.GetAllDatas(lotteryId);
        }

        public ILotteryDataList LotteryDataList(string lotteryId)
        {
            var datas = _lotteryDataQueryService.GetAllDatas(lotteryId);

            return new LotteryDataList(datas);
        }

        public IList<PredictDataDto> NewLotteryDataList(string lotteryId, int? predictPeroid, string userId)
        {
            //  var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var finalLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);

            if (!predictPeroid.HasValue)
            {
                predictPeroid = finalLotteryData.FinalPeriod + 1;
            }

            if (finalLotteryData.FinalPeriod >= predictPeroid)
            {
                throw new LotteryDataException($"预测的期数第{predictPeroid}期必须大于最后的开奖期数{finalLotteryData.FinalPeriod}");
            }

            var predictDatas = new List<PredictDataDto>();
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);
            foreach (var userNorm in userNorms)
            {
                predictDatas.AddRange(PredictNormData(lotteryId, userNorm, predictPeroid.Value));
            }
            return predictDatas;
        }



        #region 私有方法
        private IEnumerable<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm, int predictPeroid)
        {
            return _lotteryPredictDataService.PredictNormData(lotteryId, userNorm, predictPeroid);
        }


        #endregion


    }
}