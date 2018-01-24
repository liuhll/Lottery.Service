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
        /// 授权失败
        /// </summary>
        public const int AuthorizeFailed = 40002;

        public const int InvalidToken = 40003;

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
        /// 其他位置的异常
        /// </summary>
        public const int UnknownError = 50004;

    }
}