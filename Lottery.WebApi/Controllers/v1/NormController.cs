using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Norm;
using Lottery.Commands.Norms;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Norms;
using Lottery.WebApi.Validations;

namespace Lottery.WebApi.Controllers.v1
{
    /// <summary>
    /// 追号计划指标控制器
    /// </summary>
    [RoutePrefix("v1/norm")]
    public class NormController : BaseApiV1Controller
    {
        private readonly INormConfigAppService _normConfigAppService;
        private readonly IUserNormDefaultConfigService _userNormDefaultConfigService;
        private readonly UserNormDefaultConfigInputValidator _userNormDefaultConfigInputValidator;

        public NormController(ICommandService commandService, 
            INormConfigAppService normConfigAppService,
            UserNormDefaultConfigInputValidator userNormDefaultConfigInputValidator, 
            IUserNormDefaultConfigService userNormDefaultConfigService) 
            : base(commandService)
        {
            _normConfigAppService = normConfigAppService;
            _userNormDefaultConfigInputValidator = userNormDefaultConfigInputValidator;
            _userNormDefaultConfigService = userNormDefaultConfigService;
        }

        /// <summary>
        /// 用户默认的公式指标
        /// </summary>
        /// <param name="lotteryId">彩种Id</param>
        /// <returns></returns>
        [Route("usernormdefaultconfig")]
        [HttpGet]
        public UserNormDefaultConfigOutput GetUserNormDefaultConfig(string lotteryId)
        {
            return _normConfigAppService.GetUserNormDefaultConfig(_lotterySession.UserId, lotteryId);
        }

        /// <summary>
        /// 修改用户默认的公式指标
        /// </summary>
        /// <param name="input">用户配置的默认公式指标</param>
        /// <returns></returns>
        [Route("usernormdefaultconfig")]
        [HttpPut]
        public async Task<string> GetUserNormDefaultConfig(UserNormDefaultConfigInput input)
        {
            var validatorResult = await _userNormDefaultConfigInputValidator.ValidateAsync(input);
            if (!validatorResult.IsValid)
            {
                throw new LotteryDataException(validatorResult.Errors.Select(p=>p.ErrorMessage + "</br>").ToString(";"));
            }
            var userNormConfig =
                _userNormDefaultConfigService.GetUserNormConfig(_lotterySession.UserId, input.LotteryId);
            if (userNormConfig == null)
            {
                var command = new AddUserNormDefaultConfigCommand(Guid.NewGuid().ToString(), _lotterySession.UserId, input.LotteryId, input.PlanCycle, input.ForecastCount, input.UnitHistoryCount,
                    input.MinRightSeries, input.MaxRightSeries, input.MinErrortSeries, input.MaxErrortSeries, input.LookupPeriodCount, input.ExpectMinScore, input.ExpectMaxScore);
                CommandExecute(command);
            }
            else
            {
                var command = new UpdateUserNormDefaultConfigCommand(userNormConfig.Id,  input.PlanCycle, input.ForecastCount, input.UnitHistoryCount,
                    input.MinRightSeries, input.MaxRightSeries, input.MinErrortSeries, input.MaxErrortSeries, input.LookupPeriodCount, input.ExpectMinScore, input.ExpectMaxScore);
                CommandExecute(command);
            }         
            return "设置默认的公式指标成功";
        }
    }
}
