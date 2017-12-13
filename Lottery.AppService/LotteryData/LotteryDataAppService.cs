using System.Collections.Generic;
using ECommon.Components;
using Lottery.AppService.Predict;
using Lottery.Dtos.Lotteries;
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

        public LotteryDataAppService(
            ILotteryDataQueryService lotteryDataQueryService,
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataService lotteryPredictDataService)
        {
            _lotteryDataQueryService = lotteryDataQueryService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataService = lotteryPredictDataService;
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

        public IList<PredictDataDto> NewLotteryDataList(string lotteryId, int predictPeroid, string userId)
        {
            //  var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var predictDatas = new List<PredictDataDto>();
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);
            foreach (var userNorm in userNorms)
            {
                predictDatas.AddRange(PredictNormData(lotteryId, userNorm, predictPeroid));
            }
            return predictDatas;
        }

        private IEnumerable<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm,int predictPeroid)
        {
            return _lotteryPredictDataService.PredictNormData(lotteryId, userNorm, predictPeroid);
        }

        #region 私有方法



        #endregion


    }
}