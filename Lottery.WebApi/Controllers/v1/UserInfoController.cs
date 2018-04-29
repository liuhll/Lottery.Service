using ENode.Commanding;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.UserInfo;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.UserInfos;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/user")]
    public class UserInfoController : BaseApiV1Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ILotteryQueryService _lotteryQueryService;

        public UserInfoController(ICommandService commandService,
            IUserInfoService userInfoService,
            ILotteryQueryService lotteryQueryService) : base(commandService)
        {
            _userInfoService = userInfoService;
            _lotteryQueryService = lotteryQueryService;
        }

        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <returns></returns>
        [Route("me")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<EntryInfo> GetUserInfo()
        {
            var userInfo = await _userInfoService.GetUserInfoById(_lotterySession.UserId);
            var userInfoOutput = AutoMapper.Mapper.Map<UserInfoOutput>(userInfo);
            userInfoOutput.MemberRank = _userMemberRank;
            userInfoOutput.SystemType = _lotterySession.SystemType;
            userInfoOutput.SystemTypeId = _lotterySession.SystemTypeId;
            var entryInfo = new EntryInfo()
            {
                UserInfo = userInfoOutput
            };
            if (_lotterySession.SystemType == SystemType.App)
            {
                var lotteryInfo = _lotteryQueryService.GetLotteryInfoById(_lotterySession.SystemTypeId);
                entryInfo.LotteryInfo = AutoMapper.Mapper.Map<LotteryInfoOutput>(lotteryInfo);
            }
            return entryInfo;
        }
    }
}