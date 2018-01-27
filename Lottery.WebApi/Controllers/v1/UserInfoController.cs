using System.Threading.Tasks;
using System.Web.Http;
using ENode.Commanding;
using Lottery.Dtos.UserInfo;
using Lottery.QueryServices.UserInfos;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/user")]
    public class UserInfoController : BaseApiV1Controller
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(ICommandService commandService, 
            IUserInfoService userInfoService) : base(commandService)
        {
            _userInfoService = userInfoService;
        }

        [Route("me")]
        public async Task<UserInfoOutput> GetUserInfo()
        {
            var userInfo = await _userInfoService.GetUserInfoById(_lotterySession.UserId);
            var userInfoOutput = AutoMapper.Mapper.Map<UserInfoOutput>(userInfo);
            userInfoOutput.MemberRank = _lotterySession.MemberRank;
            userInfoOutput.SystemType = _lotterySession.SystemType;
            userInfoOutput.SystemTypeId = _lotterySession.SystemTypeId;
            return userInfoOutput;
        }

    }
}