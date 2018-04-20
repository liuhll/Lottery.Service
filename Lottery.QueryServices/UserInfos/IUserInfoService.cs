using Lottery.Dtos.Account;
using System.Threading.Tasks;

namespace Lottery.QueryServices.UserInfos
{
    public interface IUserInfoService
    {
        Task<UserInfoDto> GetUserInfo(string account);

        Task<UserInfoDto> GetUserInfoById(string id);
    }
}