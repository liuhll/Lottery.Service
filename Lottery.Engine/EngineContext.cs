using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine
{
    public class EngineContext
    {
        private static IDictionary<string, ILotterEngine> _lotterEngines;
        private static ILotteryQueryService _lotteryQueryService;
        private static ICollection<LotteryInfoDto> _lotteryInfos;
        static EngineContext()
        {
            
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _lotteryInfos = _lotteryQueryService.GetAllLotteryInfo();

        }

        public static void Initialize()
        {
            _lotterEngines = new Dictionary<string, ILotterEngine>();

            foreach (var lotteryInfo in _lotteryInfos)
            {
                _lotterEngines[lotteryInfo.Id] = new LotterEngine(lotteryInfo);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ILotterEngine LotterEngine(string lotteryId, bool forceRecreate = false)
        {
            if (_lotterEngines == null || forceRecreate)
            {
                Initialize();
            }
            return _lotterEngines[lotteryId];
        }


    }
}