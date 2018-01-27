using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.UserInfo;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.UserInfos;

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

        [Route("me")]
        public async Task<UserInfoOutput> GetUserInfo()
        {
            var userInfo = await _userInfoService.GetUserInfoById(_lotterySession.UserId);
            var userInfoOutput = AutoMapper.Mapper.Map<UserInfoOutput>(userInfo);
            userInfoOutput.MemberRank = _lotterySession.MemberRank;
            userInfoOutput.SystemType = _lotterySession.SystemType;
            userInfoOutput.SystemTypeId = _lotterySession.SystemTypeId;
            if (_lotterySession.SystemType == SystemType.App)
            {
                var lotteryInfo = _lotteryQueryService.GetLotteryInfoById(_lotterySession.SystemTypeId);
                userInfoOutput.LotteryInfo = AutoMapper.Mapper.Map<LotteryInfoOutput>(lotteryInfo);
            }
            return userInfoOutput;
        }

    }
}