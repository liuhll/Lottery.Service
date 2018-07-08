namespace Lottery.Infrastructure
{
    public class LotteryConstants
    {
        public const string JwtSecurityKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public const string BackOfficeKey = "BackOffice";

        public const string OfficialWebsite = "OfficialWebsite";
        public const string ValidAudience = "clmeng_lottery";
        public const string ValidIssuer = "clmeng_lottery";

        public const int HistoryPredictResultCount = 10;

        public const int ContinuousSignedDays = 5;

        public const string SystemUser = "System";

        public const string QrCodeUrl = "https://pan.baidu.com/share/qrcode?w=180&h=180&url={0}";
    }
}