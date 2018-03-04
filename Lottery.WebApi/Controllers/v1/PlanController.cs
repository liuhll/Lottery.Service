using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Norm;
using Lottery.AppService.Plan;
using Lottery.AppService.Validations;
using Lottery.Commands.Norms;
using Lottery.Dtos.Norms;
using Lottery.Dtos.Plans;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.Norms;

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
        private readonly INormConfigAppService _normConfigAppService;
        private readonly UserNormConfigInputValidator _userNormConfigInputValidator;

        public PlanController(ICommandService commandService, 
            IPlanInfoAppService planInfoAppService,
            IUserNormDefaultConfigService userNormDefaultConfigService,
            UserPlanInfoInputValidator planInfoInputValidator,
            ILotteryDataAppService lotteryDataAppService,
            INormConfigAppService normConfigAppService,
            UserNormConfigInputValidator userNormConfigInputValidator) : base(commandService)
        {
            _planInfoAppService = planInfoAppService;
            _userNormDefaultConfigService = userNormDefaultConfigService;
            _planInfoInputValidator = planInfoInputValidator;
            _lotteryDataAppService = lotteryDataAppService;
            _normConfigAppService = normConfigAppService;
            _userNormConfigInputValidator = userNormConfigInputValidator;
        }

        /// <summary>
        /// 获取用户计划接口
        /// </summary>
        /// <remarks>获取用户设置的计划类型,如果用户未设置计划，则获取系统默认的计划</remarks>
        /// <returns>用户设置的计划信息</returns>
        [HttpGet]
        [Route("userplans")]
        public UserPlanInfoDto GetUserPlans()
        {
            return _planInfoAppService.GetUserPlanInfo(LotteryInfo.Id, _lotterySession.UserId);
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

            var finalLotteryData = _lotteryDataAppService.GetFinalLotteryData(LotteryInfo.Id);

            var userDefaultNormConfig = 
                _userNormDefaultConfigService.GetUserNormOrDefaultConfig(_lotterySession.UserId, LotteryInfo.Id);

            var userNormConfigs = _normConfigAppService.GetUserNormConfig(LotteryInfo.Id, _lotterySession.UserId);
            if (userNormConfigs!= null && userNormConfigs.Any())
            {
                var deleteUserNormConfigsPlanIds = userNormConfigs.Select(p => p.PlanId).Except(input.PlanIds.Select(p=>p.PlanId));
                var deleteUserNormConfigs =
                    userNormConfigs.Where(p => deleteUserNormConfigsPlanIds.Any(q => q == p.PlanId));
                // 移出本次未选中但是之前选中的计划
                foreach (var config in deleteUserNormConfigs)
                {
                    await SendCommandAsync(new DeteteNormConfigCommand(config.Id));
                }
            }
           
            foreach (var plan in input.PlanIds)
            {
                // 如果用户选中的计划则忽略
                if (userNormConfigs!= null && userNormConfigs.Any(p=>p.PlanId == plan.PlanId))
                {
                    continue;
                }
                var command = new AddNormConfigCommand(Guid.NewGuid().ToString(),_lotterySession.UserId,
                    LotteryInfo.Id, plan.PlanId, userDefaultNormConfig.PlanCycle,
                    userDefaultNormConfig.ForecastCount, finalLotteryData.Period,
                    userDefaultNormConfig.UnitHistoryCount, userDefaultNormConfig.MinRightSeries,
                    userDefaultNormConfig.MaxRightSeries, userDefaultNormConfig.MinErrortSeries,
                    userDefaultNormConfig.MaxErrortSeries, userDefaultNormConfig.LookupPeriodCount,
                    userDefaultNormConfig.ExpectMinScore, userDefaultNormConfig.ExpectMaxScore, plan.Sort);
                await SendCommandAsync(command);
            }

            return "更改计划成功";
        }

        /// <summary>
        /// 获取计划指标配置接口1
        /// </summary>
        /// <remarks>通过指标Id获取计划公式指标配置</remarks>
        /// <param name="normId">指标Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("userplannorm1")]
        public UserPlanNormOutput GetUserPlanByNormId(string normId)
        {
            return _normConfigAppService.GetUserNormConfigById(_lotterySession.UserId, normId);
        }

        /// <summary>
        /// 获取计划指标配置接口2
        /// </summary>
        /// <remarks>通过计划Id获取计划公式指标配置</remarks>
        /// <param name="planId">计划Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("userplannorm2")]
        public UserPlanNormOutput GetUserPlanByPlanId(string planId)
        {
            return _normConfigAppService.GetUserNormConfigByPlanId(_lotterySession.UserId, LotteryInfo.Id, planId);
        }

        /// <summary>
        /// 更新用户指定的计划公式指标
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("userplannorm")]
        public async Task<string> UpdateUserPlanNorm(UserNormPlanConfigInput input)
        {
            var validatorResult = await _userNormConfigInputValidator.ValidateAsync(input);
            if (!validatorResult.IsValid)
            {
                throw new LotteryDataException(validatorResult.Errors.Select(p => p.ErrorMessage + "</br>").ToString(";"));
            }

            // todo: 更严格的指标公式验证

            var userPlanNorm = _normConfigAppService.GetUserNormConfigByPlanId(_lotterySession.UserId, LotteryInfo.Id, input.PlanId);
            var finalLotteryData = _lotteryDataAppService.GetFinalLotteryData(LotteryInfo.Id);
            var command = new UpdateNormConfigCommand(userPlanNorm.Id, _lotterySession.UserId, LotteryInfo.Id, input.PlanId,
                input.PlanCycle, input.ForecastCount, finalLotteryData.Period,
                input.UnitHistoryCount,input.MinRightSeries, input.MaxRightSeries, 
                input.MinErrortSeries, input.MaxErrortSeries, input.LookupPeriodCount,
                input.ExpectMinScore, input.ExpectMaxScore,input.CustomNumbers);
            await SendCommandAsync(command);
            return "设置公式指标成功";
         
        }
    }
}