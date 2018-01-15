using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Plan;
using Lottery.Commands.Norms;
using Lottery.Dtos.Plans;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.Norms;
using Lottery.WebApi.Validations;

namespace Lottery.WebApi.Controllers.v1
{
    /// <summary>
    /// 彩票计划相关控制器
    /// </summary>
    [RoutePrefix("v1/plan")]
    public class PlanController : BaseApiV1Controller
    {
        private readonly IPlanInfoAppService _planInfoAppService;
        private readonly IUserNormDefaultConfigService _userNormDefaultConfigService;
        private readonly UserPlanInfoInputValidator _planInfoInputValidator;
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly INormConfigQueryService _normConfigQueryService;

        public PlanController(ICommandService commandService, 
            IPlanInfoAppService planInfoAppService,
            IUserNormDefaultConfigService userNormDefaultConfigService,
            UserPlanInfoInputValidator planInfoInputValidator,
            ILotteryDataAppService lotteryDataAppService,
            INormConfigQueryService normConfigQueryService) : base(commandService)
        {
            _planInfoAppService = planInfoAppService;
            _userNormDefaultConfigService = userNormDefaultConfigService;
            _planInfoInputValidator = planInfoInputValidator;
            _lotteryDataAppService = lotteryDataAppService;
            _normConfigQueryService = normConfigQueryService;
        }

        /// <summary>
        /// 获取用户计划接口
        /// </summary>
        /// <remarks>获取用户设置的计划类型,如果用户未设置计划，则获取系统默认的计划</remarks>
        /// <param name="lotteryId">彩种Id</param>
        /// <returns>用户设置的计划信息</returns>
        [HttpGet]
        [Route("userplans")]
        public UserPlanInfoDto GetUserPlans(string lotteryId)
        {
            return _planInfoAppService.GetUserPlanInfo(lotteryId, _lotterySession.UserId);
        }

        /// <summary>
        /// 更改用户计划
        /// </summary>
        /// <remarks>更改用户计划接口</remarks>
        /// <param name="input">用户选择的计划</param>
        /// <returns>用户设置的计划信息</returns>
        [HttpPut]
        [Route("userplans")]
        public async Task<string> UpdateUserPlans(UserPlanInfoInput input)
        {
            var validatorResult = await _planInfoInputValidator.ValidateAsync(input);
            if (!validatorResult.IsValid)
            {
                throw new LotteryDataException(validatorResult.Errors.Select(p => p.ErrorMessage + "</br>").ToString(";"));
            }

            var finalLotteryData = _lotteryDataAppService.GetFinalLotteryData(input.LotteryId);

            var userDefaultNormConfig = 
                _userNormDefaultConfigService.GetUserNormOrDefaultConfig(_lotterySession.UserId, input.LotteryId);

            var userNormConfigs = _normConfigQueryService.GetUserNormConfig(input.LotteryId, _lotterySession.UserId);
            if (userNormConfigs!= null && userNormConfigs.Any())
            {
                var deleteUserNormConfigsPlanIds = userNormConfigs.Select(p => p.PlanId).Except(input.PlanIds);
                var deleteUserNormConfigs =
                    userNormConfigs.Where(p => deleteUserNormConfigsPlanIds.Any(q => q == p.PlanId));
                // 移出本次未选中但是之前选中的计划
                foreach (var config in deleteUserNormConfigs)
                {
                    await SendCommandAsync(new DeteteNormConfigCommand(config.Id));
                }
            }
           
            foreach (var planId in input.PlanIds)
            {
                // 如果用户选中的计划则忽略
                if (userNormConfigs!= null && userNormConfigs.Any(p=>p.PlanId == planId))
                {
                    continue;
                }
                var command = new AddNormConfigCommand(Guid.NewGuid().ToString(),_lotterySession.UserId, 
                    input.LotteryId,planId, userDefaultNormConfig.PlanCycle,
                    userDefaultNormConfig.ForecastCount, finalLotteryData.Period,
                    userDefaultNormConfig.UnitHistoryCount, userDefaultNormConfig.MinRightSeries,
                    userDefaultNormConfig.MaxRightSeries, userDefaultNormConfig.MinErrortSeries,
                    userDefaultNormConfig.MaxErrortSeries, userDefaultNormConfig.LookupPeriodCount,
                    userDefaultNormConfig.ExpectMinScore, userDefaultNormConfig.ExpectMaxScore);
                await SendCommandAsync(command);
            }

            return "更改计划成功";
        }
    }
}