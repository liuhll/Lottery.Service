using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Operations;
using Lottery.AppService.Validations.Opinions;
using Lottery.Commands.OpinionRecords;
using Lottery.Dtos.OnlineHelp;
using Lottery.Dtos.Opinions;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/operation")]
    public class OperationController : BaseApiV1Controller
    {
        private readonly OpinionInputValidtor _opinionInputValidtor;
        private readonly IOnlineHelpAppService _onlineHelpAppService;

        public OperationController(ICommandService commandService,
            OpinionInputValidtor opinionInputValidtor,
            IOnlineHelpAppService onlineHelpAppService) 
            : base(commandService)
        {
            _opinionInputValidtor = opinionInputValidtor;
            _onlineHelpAppService = onlineHelpAppService;
        }

        [Route("v1/activity")]
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
        [Route("v1/opinion")]
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
        [Route("v1/onlinehelps")]
        [AllowAnonymous]
        public async Task<ICollection<OnlineGroupOutput>> GetOnlineHelps(string lotteryCode)
        {
            if (lotteryCode.IsNullOrEmpty())
            {
                throw new LotteryDataException("彩种类型不允许为空");
            }
            return _onlineHelpAppService.GetOnlineHelps(lotteryCode);
        }

    }
}