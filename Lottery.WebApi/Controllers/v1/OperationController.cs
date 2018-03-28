using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.AppService.Validations.Opinions;
using Lottery.Commands.OpinionRecords;
using Lottery.Dtos.Opinions;
using Lottery.Infrastructure.Exceptions;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/operation")]
    public class OperationController : BaseApiV1Controller
    {
        private readonly OpinionInputValidtor _opinionInputValidtor;
        public OperationController(ICommandService commandService,
            OpinionInputValidtor opinionInputValidtor) 
            : base(commandService)
        {
            _opinionInputValidtor = opinionInputValidtor;
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

    }
}