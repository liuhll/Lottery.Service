using System;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using Lottery.QueryServices.IdentifyCodes;

namespace Lottery.AppService.IdentifyCode
{
    [Component]
    public class IdentifyCodeAppService : IIdentifyCodeAppService
    {
        private readonly int _identifyCodeDuration = 0;
        private readonly IIdentifyCodeQueryService _identifyCodeQueryService;

        public IdentifyCodeAppService(ICacheManager cacheManager, 
            IIdentifyCodeQueryService identifyCodeQueryService)
        {
            _identifyCodeQueryService = identifyCodeQueryService;
            _identifyCodeDuration = ConfigHelper.ValueInt("IdentifyCodeDuration");
        }

        public IdentifyCodeOutput GenerateIdentifyCode(string account, AccountRegistType accountType)
        {          
            var identifyCode = _identifyCodeQueryService.GetIdentifyCode(account);
            var code = RandomHelper.GenerateIdentifyCode();
            var output = new IdentifyCodeOutput
            {
                IdentifyCodeId = identifyCode == null ? Guid.NewGuid().ToString() : identifyCode.Id,
                Code = code,
                ExpirationDate = DateTime.Now.AddMinutes(_identifyCodeDuration),
                IsNew = identifyCode == null,
            };

            return output;

        }

        public IdentifyCodeValidOutput ValidIdentifyCode(string account, string identifyCode)
        {
            if (string.IsNullOrEmpty(identifyCode))
            {
                throw new LotteryDataException("请输入验证码");
            }
            var identifyCodeDto = _identifyCodeQueryService.GetIdentifyCode(account);
            if (identifyCodeDto == null)
            {
                throw new LotteryDataException("请获取验证码");
            }
            if (identifyCodeDto.ExpirationDate < DateTime.Now)
            {
                return new IdentifyCodeValidOutput()
                {
                    IdentifyCodeId = identifyCodeDto.Id,
                    IsValid = false,
                    IsOvertime = true
                };
            }
            if (identifyCodeDto.Code != identifyCode)
            {
                return new IdentifyCodeValidOutput()
                {
                    IdentifyCodeId = identifyCodeDto.Id,
                    IsValid = false,
                    IsOvertime = false
                };
            }
            return new IdentifyCodeValidOutput()
            {
                IdentifyCodeId = identifyCodeDto.Id,
                IsValid = true,
                IsOvertime = false
            };

        }
    }
}