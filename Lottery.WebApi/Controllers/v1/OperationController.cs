using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Operations;
using Lottery.AppService.Validations.Opinions;
using Lottery.Commands.OpinionRecords;
using Lottery.Dtos.AppInfo;
using Lottery.Dtos.OnlineHelp;
using Lottery.Dtos.Opinions;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.AppInfos;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/operation")]
    public class OperationController : BaseApiV1Controller
    {
        private readonly OpinionInputValidtor _opinionInputValidtor;
        private readonly IOnlineHelpAppService _onlineHelpAppService;
        private readonly IAppInfoQueryService _appInfoQueryService;

        public OperationController(ICommandService commandService,
            OpinionInputValidtor opinionInputValidtor,
            IOnlineHelpAppService onlineHelpAppService,
            IAppInfoQueryService appInfoQueryService) 
            : base(commandService)
        {
            _opinionInputValidtor = opinionInputValidtor;
            _onlineHelpAppService = onlineHelpAppService;
            _appInfoQueryService = appInfoQueryService;
        }

        [Route("activity")]
        public string Create()
        {
            return "";
        }

        /// <summary>
        /// 新增反馈意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("opinion")]
        [AllowAnonymous]
        public async Task<string> CreateOpinion(OpinionInput input)
        {
            var validResult =await _opinionInputValidtor.ValidateAsync(input);
            if (!validResult.IsValid)
            {
                throw new LotteryDataException(validResult.Errors.First().ErrorMessage);
            }
            await SendCommandAsync(new AddOpinionRecordCommand(Guid.NewGuid().ToString(), input.OpinionType,
                input.Content, input.Platform, input.ContactWay, _lotterySession.UserId)); 
            return "您的意见意见被收录,感谢您的反馈!";
        }

        /// <summary>
        /// 获取在线帮助
        /// </summary>
        /// <param name="lotteryCode">彩种编码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("onlinehelps")]
        [AllowAnonymous]
        public async Task<ICollection<OnlineGroupOutput>> GetOnlineHelps(string lotteryCode)
        {
            if (lotteryCode.IsNullOrEmpty())
            {
                throw new LotteryDataException("彩种类型不允许为空");
            }
            return _onlineHelpAppService.GetOnlineHelps(lotteryCode);
        }

        /// <summary>
        /// 获取App信息
        /// </summary>
        /// <param name="platforms"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("appinfo")]
        [AllowAnonymous]
        public async Task<AppInfoOutput> GetAppInfo(AppPlatform platforms)
        {
            return _appInfoQueryService.GetAppInfo(platforms);
        }

    }
}