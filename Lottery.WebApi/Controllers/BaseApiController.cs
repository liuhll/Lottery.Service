using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ECommon.Components;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Lotteries;

namespace Lottery.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly ICommandService _commandService;
        protected readonly ILotterySession _lotterySession;        
        private readonly ILotteryQueryService _lotteryQueryService;

        protected LotteryInfoDto _lotteryInfo;
        protected LotteryInfoDto LotteryInfo
        {
            get
            {
                if (_lotterySession.SystemType == SystemType.App)
                {
                    return _lotteryInfo;
                }
                throw new LotteryException("非移动端无法获取彩种信息");
            }
        }

        protected BaseApiController(ICommandService commandService)
        {
            _commandService = commandService;
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();          
            _lotterySession = NullLotterySession.Instance;
            if (_lotterySession.SystemType == SystemType.App)
            {
                InitLotteryInfo();
            }
        }

        private void InitLotteryInfo()
        {
            _lotteryInfo = _lotteryQueryService.GetLotteryInfoById(_lotterySession.SystemTypeId);
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
