using System.Threading.Tasks;
using Lottery.Dtos.Account;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserInfoService
    {
        Task<UserInfoDto> GetUserInfo(string account);

        Task<UserInfoDto> GetUserInfoById(string id);
    }
}