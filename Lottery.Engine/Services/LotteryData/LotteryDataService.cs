using System.Collections.Generic;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.Services.LotteryData
{
    [Component]
    public class LotteryDataService : ILotteryDataService
    {
        private readonly ILotteryDataQueryService _lotteryDataQueryService;

        public LotteryDataService(ILotteryDataQueryService lotteryDataQueryService)
        {
            _lotteryDataQueryService = lotteryDataQueryService;
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

        public IList<PredictDataDto> GetLotteryDataList(int peroid, NormConfigDto norm)
        {
            throw new System.NotImplementedException();
        }
    }
}