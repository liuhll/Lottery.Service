using ENode.Commanding;
using Lottery.AppService.Operations;
using Lottery.AppService.Validations.Opinions;
using Lottery.Commands.OpinionRecords;
using Lottery.Commands.Points;
using Lottery.Dtos.AppInfo;
using Lottery.Dtos.CustomService;
using Lottery.Dtos.OnlineHelp;
using Lottery.Dtos.Opinions;
using Lottery.Dtos.Points;
using Lottery.Dtos.Wechat;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.AppInfos;
using Lottery.QueryServices.CustomService;
using Lottery.QueryServices.Points;
using Lottery.QueryServices.UserInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/operation")]
    public class OperationController : BaseApiV1Controller
    {
        private readonly OpinionInputValidtor _opinionInputValidtor;
        private readonly IOnlineHelpAppService _onlineHelpAppService;
        private readonly IAppInfoQueryService _appInfoQueryService;
        private readonly IPointQueryService _pointQueryService;
        private readonly IUserInfoService _userInfoService;
        private readonly ICustomServiceQueryService _customServiceQueryService;

        public OperationController(ICommandService commandService,
            OpinionInputValidtor opinionInputValidtor,
            IOnlineHelpAppService onlineHelpAppService,
            IAppInfoQueryService appInfoQueryService,
            IPointQueryService pointQueryService,
            IUserInfoService userInfoService,
            ICustomServiceQueryService customServiceQueryService)
            : base(commandService)
        {
            _opinionInputValidtor = opinionInputValidtor;
            _onlineHelpAppService = onlineHelpAppService;
            _appInfoQueryService = appInfoQueryService;
            _pointQueryService = pointQueryService;
            _userInfoService = userInfoService;
            _customServiceQueryService = customServiceQueryService;
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
            var validResult = await _opinionInputValidtor.ValidateAsync(input);
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
        /// <param name="platform"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("appinfo")]
        [AllowAnonymous]
        public async Task<AppInfoOutput> GetAppInfo(AppPlatform platform)
        {
            return _appInfoQueryService.GetAppInfo(platform);
        }

        /// <summary>
        /// 获取客服信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customservice")]
        [AllowAnonymous]
        public async Task<CustomServiceOutput> GetCustomService()
        {
            return _customServiceQueryService.GetCustomService(_lotterySession.SystemTypeId);
        }

        /// <summary>
        /// 签到接口
        /// </summary>
        /// <remarks>连续签到五日,可获得额外积分</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("signed")]
        [AllowAnonymous]
        public async Task<SignedInfoOutput> Signed()
        {
            var signedPointInfo = _pointQueryService.GetPointInfoByType(PointType.Signed);
            var todaySignedInfo = _pointQueryService.GetTodaySigned(_lotterySession.UserId);
            if (todaySignedInfo != null)
            {
                throw new LotteryException("您今天已经签到,无法重复签到");
            }
            var sinedNotes = $"{DateTime.Now.ToString("yyyy-MM-dd")}日,每日签到";
            await SendCommandAsync(new AddPointRecordCommand(Guid.NewGuid().ToString(), signedPointInfo.Point,
                PointType.Signed, PointOperationType.Increase, sinedNotes, _lotterySession.UserId));

            var signeds = _pointQueryService.GetUserLastSined(_lotterySession.UserId);
            if (signeds != null && signeds.CurrentPeriodEndDate.Date == DateTime.Now.Date && signeds.DurationDays != 0 && signeds.DurationDays % LotteryConstants.ContinuousSignedDays == 0)
            {
                var signAdditionalPointInfo = _pointQueryService.GetPointInfoByType(PointType.SignAdditional);
                var signAdditionalNotes = $"{DateTime.Now.ToString("yyyy-MM-dd")}日,连续签到五日,获得额外{signAdditionalPointInfo.Point}点积分";
                await SendCommandAsync(new AddPointRecordCommand(Guid.NewGuid().ToString(), signAdditionalPointInfo.Point,
                    PointType.SignAdditional, PointOperationType.Increase, signAdditionalNotes, _lotterySession.UserId));
            }
            Thread.Sleep(200);
            return await SignedInfo();
        }

        /// <summary>
        /// 获取用户签到信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("signedinfo")]
        [AllowAnonymous]
        public async Task<SignedInfoOutput> SignedInfo()
        {
            var output = new SignedInfoOutput()
            {
                DurationDays = 0,
                TodayIsSiged = false,
            };
            var todaySignedInfo = _pointQueryService.GetTodaySigned(_lotterySession.UserId);
            if (todaySignedInfo != null)
            {
                output.TodayIsSiged = true;
            }
            var signeds = _pointQueryService.GetUserLastSined(_lotterySession.UserId);
            if (signeds != null)
            {
                // output.DurationDays = signeds.DurationDays;
                output.LastSignedTime = signeds.CurrentPeriodEndDate;
                var ts = DateTime.Now - signeds.CurrentPeriodEndDate;
                if (ts.Days <= 1)
                {
                    output.DurationDays = signeds.DurationDays;
                }
            }
            var userInfo = await _userInfoService.GetUserInfoById(_lotterySession.UserId);
            output.Points = userInfo.Points;
            return output;
        }

        /// <summary>
        /// 获取用户签到列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("signedlist")]
        [AllowAnonymous]
        public ICollection<PointRecordOutput> SignedList()
        {
            return _pointQueryService.GetSignedList(_lotterySession.UserId);
        }

        /// <summary>
        /// 获取微信分享配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("wechatconfig")]
        [AllowAnonymous]
        public WechatConfigOutput WechatConfig()
        {
            return new WechatConfigOutput()
            {
                Debug = true,
                Appid = "wx24329604dda9121a",
                Timestamp = DateTime.Now.ToTimeStamp(),
                Signature = "9420aa6f3c3fd0a1c5dc10621c262ec1",
                JsApiList = new List<string>()
                {
                    "onMenuShareTimeline",
                    "onMenuShareAppMessage"
                },
            };
        }
    }
}