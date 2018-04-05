using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ECommon.Components;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.Predictor;
using Lottery.Engine.TimeRule;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Reflection;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine
{
    public class LotterEngine : ILotterEngine
    {
        private readonly LotteryInfoDto _lotteryInfo;
        private readonly ITimeRuleManager _timeRuleManager;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly ILotteryFinalDataQueryService _finalDataQueryService;
        private readonly ITypeFinder _typeFinder;

        private IDictionary<AlgorithmType, IPerdictor> _perdictors = new Dictionary<AlgorithmType, IPerdictor>();

        public LotterEngine(LotteryInfoDto lotteryInfo)
        {
            _lotteryInfo = lotteryInfo;
            _timeRuleManager = new TimeRuleManager(_lotteryInfo);
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();
            _finalDataQueryService = ObjectContainer.Resolve<ILotteryFinalDataQueryService>();
            _typeFinder = ObjectContainer.Resolve<ITypeFinder>();
   
            InitializationPerdictor();
        }

        private void InitializationPerdictor()
        {
            //var predictors = _typeFinder.Find(p => p.IsSubclassOf(typeof(BasePredictor)))
            //    .Select(type=> Activator.CreateInstance(type,new [] { LotteryInfo }) as IPerdictor).ToList();
            //foreach (var predictor in predictors)
            //{
            //    _perdictors[predictor.PredictCode.ToUpper()] = predictor;
            //}

            _perdictors[AlgorithmType.DiscreteMarkov] = new DiscreteMarkovPredictor(LotteryInfo,AlgorithmType.DiscreteMarkov);
            _perdictors[AlgorithmType.Mock] = new MockPredictor(LotteryInfo,AlgorithmType.Mock);
          //  _perdictors[AlgorithmType.Stochastic] = new StochasticPredictor(LotteryInfo,AlgorithmType.Stochastic);
            _perdictors[AlgorithmType.Temperature] = new TemperatureMarkovPredictor(LotteryInfo, AlgorithmType.Temperature);
        }

        public LotteryInfoDto LotteryInfo => _lotteryInfo;


        public ITimeRuleManager TimeRuleManager => _timeRuleManager;



        public IPerdictor GetPerdictor(AlgorithmType algorithmType)
        {
            IPerdictor perdictor = _perdictors[algorithmType];  
            return perdictor;
        }
    }
}