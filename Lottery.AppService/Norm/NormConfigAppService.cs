using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Exceptions;
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
            var userDefaultConfig = _normDefaultConfigService.GetUserNormOrDefaultConfig(userId,lotteryId);
            SetSelectedLotteryNumbers(lotteryId, userDefaultConfig);
            return userDefaultConfig;
        }

       
        public ICollection<NormConfigDto> GetUserNormConfig(string lotteryId, string userId)
        {
            return _normConfigQueryService.GetUserNormConfigs(lotteryId, userId);
        }

        public UserPlanNormOutput GetUserNormConfigById(string userId, string normId)
        {
            try
            {
                var userplanNorm = _normConfigQueryService.GetUserNormConfigById(userId, normId);
                SetSelectedLotteryNumbers(userplanNorm.LotteryId,userplanNorm);
                return userplanNorm;
            }
            catch
            {
                throw new LotteryDataException("获取公式指标配置异常");
            }           
        }

        public UserPlanNormOutput GetUserNormConfigByPlanId(string userId, string lotteryId, string planId)
        {
            try
            {
                var userplanNorm = _normConfigQueryService.GetUserNormConfigByPlanId(userId, lotteryId,planId);
                if (userplanNorm == null)
                {
                    var userDefaultConfig = _normDefaultConfigService.GetUserNormOrDefaultConfig(userId, lotteryId);
                    userplanNorm = AutoMapper.Mapper.Map<UserPlanNormOutput>(userDefaultConfig);
                    userplanNorm.LotteryId = lotteryId;
                }
                SetSelectedLotteryNumbers(userplanNorm.LotteryId, userplanNorm);
                return userplanNorm;
            }
            catch(Exception ex)
            {
                
                throw new LotteryDataException("获取公式指标配置异常");
            }
        }

        #region private methods
        private void SetSelectedLotteryNumbers(string lotteryId, UserNormDefaultConfigOutput normConfig)
        {
            var lotteryPositions = _positionInfoQueryService.GetLotteryPositions(lotteryId);
            var minNumber = lotteryPositions.OrderByDescending(p => p.MinValue).First().MinValue;
            var maxNumber = lotteryPositions.OrderBy(p => p.MaxValue).First().MaxValue;
            if (string.IsNullOrEmpty(normConfig.CustomNumbers))
            {
                normConfig.LotteryNumbers = new List<LotteryNumber>();
                for (int i = minNumber; i <= maxNumber; i++)
                {
                    normConfig.LotteryNumbers.Add(new LotteryNumber()
                    {
                        Number = i,
                        IsSelected = true,
                    });
                }
                var customerNums = normConfig.LotteryNumbers.Select(p => p.Number).ToString(",");
                normConfig.CustomNumbers = customerNums.Substring(0, customerNums.Length);
            }
            else
            {
                var selectedNumbers = normConfig.CustomNumbers.Split(',').Select(p => Convert.ToInt32(p));
                normConfig.LotteryNumbers = new List<LotteryNumber>();
                for (int i = minNumber; i <= maxNumber; i++)
                {
                    normConfig.LotteryNumbers.Add(new LotteryNumber()
                    {
                        Number = i,
                        IsSelected = selectedNumbers.Any(p => p == i),
                    });
                }
            }
        }
        #endregion
    }
}