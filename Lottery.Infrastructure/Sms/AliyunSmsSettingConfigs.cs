using System;
using Aliyun.Acs.Core;
using Lottery.Infrastructure.Tools;

namespace Lottery.Infrastructure.Sms
{
    public static class AliyunSmsSettingConfigs
    {
        private static string accessId = ConfigHelper.Value("AliyunSmsAccessId");
        private static string accessSecret = ConfigHelper.Value("AliyunSmsAccessSecretId");
        private static string queueName;
        private static string signName = ConfigHelper.Value("SmsSignName");
        private static string messageType = "SmsReport";//消息类型目前有4种. 短信回执:SmsReport; 短信上行:SmsUp; 语音回执:VoiceReport; 流量回执:FlowReport;
      //  private static string queueNameTemplet = "Alicom-Queue-{0}-SmsReport";// 短信上行的队列名称. 格式是 "前缀(Alicom-Queue-)+阿里云uid+消息类型"
        private static string domainForPop = "dysmsapi.aliyuncs.com";
        private static string regionIdForPop = "cn-hangzhou";
        private static string productName = "Dysmsapi";

        public static string AccessId { get; } = accessId;

        public static string AccessSecret { get; } = accessSecret;

        public static string SignName { get; } = signName;


        //  public static string QueueNameTemplet { get; } = queueNameTemplet;

        public static string DomainForPop { get; } = domainForPop;

        public static string MessageType { get; } = messageType;

        public static string RegionIdForPop { get; } = regionIdForPop;

        public static string ProductName { get; } = productName;


    }
}
