using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.UserInfo
{
    public class UserInfoOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名(账号)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        public string QQCode { get; set; }

        /// <summary>
        ///微信账号
        /// </summary>
        public string Wechat { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// 积分余额
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 已消费的所有金额
        /// </summary>
        public int TotalConsumeAccount { get; set; }

        /// <summary>
        /// 已消费的所有积分
        /// </summary>
        public int TotalConsumePoint { get; set; }

        /// <summary>
        /// 登录的系统类型
        /// </summary>
        public SystemType SystemType { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public MemberRank MemberRank { get; set; }

        /// <summary>
        /// 如果用户登录的是移动客户,则SystemTypeId=LotteryId
        /// </summary>

        public string SystemTypeId { get; set; }
    }
}