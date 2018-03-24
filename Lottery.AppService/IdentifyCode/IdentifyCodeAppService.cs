using System;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure.Enums;
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

        public IdentifyCodeValidOutput GenerateIdentifyCode(string account, AccountRegistType accountType)
        {          
            var idnetifyCode = _identifyCodeQueryService.GetIdentifyCode(account);
            var code = RandomHelper.GenerateIdentifyCode();
            var output = new IdentifyCodeValidOutput
            {
                IdentifyCodeId = idnetifyCode == null ? Guid.NewGuid().ToString() : idnetifyCode.Id,
                Code = code,
                ExpirationDate = DateTime.Now.AddMinutes(_identifyCodeDuration),
                IsNew = idnetifyCode == null,
            };

            return output;

        }
    }
}