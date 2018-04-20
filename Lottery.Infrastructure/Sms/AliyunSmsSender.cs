using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using ECommon.Components;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using System;

namespace Lottery.Infrastructure.Sms
{
    [Component]
    public class AliyunSmsSender : ISmsSender
    {
        private readonly IClientProfile _clientProfile;
        private readonly IAcsClient _acsClient;

        public AliyunSmsSender()
        {
            _clientProfile = DefaultProfile.GetProfile(AliyunSmsSettingConfigs.RegionIdForPop, AliyunSmsSettingConfigs.AccessId, AliyunSmsSettingConfigs.AccessSecret); ;
            DefaultProfile.AddEndpoint(AliyunSmsSettingConfigs.RegionIdForPop, AliyunSmsSettingConfigs.RegionIdForPop, AliyunSmsSettingConfigs.ProductName, AliyunSmsSettingConfigs.DomainForPop);
            _acsClient = new DefaultAcsClient(_clientProfile);
        }

        public void Send(string to, string content)
        {
            try
            {
                var defaultSmsCode = ConfigHelper.Value("DefaultSmsCode");
                Send(to, content, defaultSmsCode);
            }
            catch (Exception e)
            {
                throw new LotteryException("获取默认的SmsCode失败", ErrorCode.DataError);
            }
        }

        public void Send(string to, string templateParam, string templateCode)
        {
            try
            {
                var request = new SendSmsRequest()
                {
                    PhoneNumbers = to,
                    SignName = AliyunSmsSettingConfigs.SignName,
                    TemplateCode = templateCode,
                    TemplateParam = templateParam,
                    OutId = "Clmeng"
                };
                var result = _acsClient.GetAcsResponse(request);
                if (result.Code == "")
                {
                }
            }
            catch (ServerException e)
            {
                throw new LotteryException("短信服务端异常", e);
            }
            catch (ClientException e)
            {
                throw new LotteryException("短信客户端异常", e);
            }
        }
    }
}