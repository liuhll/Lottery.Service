namespace Lottery.Infrastructure
{
    public class ErrorCode
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 认证失败
        /// </summary>
        public const int AuthorizationFailed = 40001;

        /// <summary>
        /// 账号被冻结
        /// </summary>
        public const int AccountFrozen = 40004;

        /// <summary>
        /// 授权失败
        /// </summary>
        public const int AuthorizeFailed = 40002;

        /// <summary>
        /// 无效的Token
        /// </summary>
        public const int InvalidToken = 40003;

        public const int OvertimeToken = 40004;

        /// <summary>
        /// 超过允许的最大客户端数
        /// </summary>
        public const int OverloadPermitClientCount = 40005;

        /// <summary>
        /// 数据错误异常
        /// </summary>
        public const int DataError = 50001;

        /// <summary>
        /// 数据验证异常
        /// </summary>
        public const int ValidateError = 2000;

        /// <summary>
        /// 业务处理异常
        /// </summary>
        public const int BusinessError = 50003;

        /// <summary>
        /// 其他未知的异常
        /// </summary>
        public const int UnknownError = 50004;

    }
}