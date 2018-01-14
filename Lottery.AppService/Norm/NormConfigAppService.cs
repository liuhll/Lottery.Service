using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure.Collections;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.Norms;

namespace Lottery.AppService.Norm
{
    [Component]
    public class NormConfigAppService : INormConfigAppService
    {
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly IUserNormDefaultConfigService _normDefaultConfigService;
        private readonly IPositionInfoQueryService _positionInfoQueryService;

        public NormConfigAppService(INormConfigQueryService normConfigQueryService,
            IUserNormDefaultConfigService normDefaultConfigService,
            IPositionInfoQueryService positionInfoQueryService)
        {
            _normConfigQueryService = normConfigQueryService;
            _normDefaultConfigService = normDefaultConfigService;
            _positionInfoQueryService = positionInfoQueryService;
        }

        public ICollection<NormConfigDto> GetNormConfigsByUserIdOrDefault(string userId = "")
        {
            return _normConfigQueryService.GetUserOrDefaultNormConfigs(userId);
        }

        public UserNormDefaultConfigOutput GetUserNormDefaultConfig(string userId, string lotteryId)
        {
            var userDefaultConfig = _normDefaultConfigService.GetUserNormDefaultConfig(userId,lotteryId);
            var lotteryPositions = _positionInfoQueryService.GetLotteryPositions(lotteryId);
            var minNumber = lotteryPositions.OrderByDescending(p => p.MinValue).First().MinValue;
            var maxNumber = lotteryPositions.OrderBy(p => p.MaxValue).First().MaxValue;
            if (string.IsNullOrEmpty(userDefaultConfig.CustomNumbers))
            {
                
                userDefaultConfig.LotteryNumbers = new List<LotteryNumber>();
                for (int i = minNumber; i <= maxNumber; i++)
                {
                    userDefaultConfig.LotteryNumbers.Add(new LotteryNumber()
                    {
                        Number = i,
                        IsSelected = true,
                    });
                }
                var customerNums = userDefaultConfig.LotteryNumbers.Select(p => p.Number).ToString(",");
                userDefaultConfig.CustomNumbers = customerNums.Substring(0, customerNums.Length - 1);
            }
            else
            {
                var selectedNumbers = userDefaultConfig.CustomNumbers.Split(',').Select(p => Convert.ToInt32(p));
                userDefaultConfig.LotteryNumbers = new List<LotteryNumber>();
                for (int i = minNumber; i <= maxNumber; i++)
                {
                    userDefaultConfig.LotteryNumbers.Add(new LotteryNumber()
                    {
                        Number = i,
                        IsSelected = selectedNumbers.Any(p=> p == i),
                    });
                }
            }
            return userDefaultConfig;
        }
    }
}