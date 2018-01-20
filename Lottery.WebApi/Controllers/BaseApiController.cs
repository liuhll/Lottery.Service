using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Lottery.Infrastructure.RunTime.Session;

namespace Lottery.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly ICommandService _commandService;
        protected readonly ILotterySession _lotterySession;

        protected BaseApiController(ICommandService commandService)
        {
            _commandService = commandService;
            _lotterySession = NullLotterySession.Instance;
        }

        /// <summary>异步发送给定的命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        protected Task<AsyncTaskResult> SendCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.SendAsync(command).TimeoutAfter(millisecondsDelay);
        }

        /// <summary> 执行命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        protected CommandResult CommandExecute(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.Execute(command, millisecondsDelay);
        }
    }
}
