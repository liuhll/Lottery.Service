using ECommon.Extensions;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Result;
using System.Linq;
using System.Web.Http.Controllers;

namespace Lottery.WebApi.Dynamic
{
    public class LotteryApiControllerActionSelector : ApiControllerActionSelector
    {
        private readonly ILotteryApiConfiguration _lotteryApiConfiguration;

        public LotteryApiControllerActionSelector(ILotteryApiConfiguration lotteryApiConfiguration)
        {
            _lotteryApiConfiguration = lotteryApiConfiguration;
        }

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            var httpActionDescriptor = base.SelectAction(controllerContext);

            var wrapResultAttributes = httpActionDescriptor.GetCustomAttributes<WrapResultAttribute>();

            if (wrapResultAttributes.Safe().Any())
            {
                httpActionDescriptor.Properties["__LotteryApiDontWrapResultAttribute"] =
                    wrapResultAttributes.First().GetType();
            }
            else if (_lotteryApiConfiguration.SetDefaultWrapResult)
            {
                httpActionDescriptor.Properties["__LotteryApiDontWrapResultAttribute"] =
                    _lotteryApiConfiguration.DefaultWrapResultAttribute.GetType();
            }

            return httpActionDescriptor;
        }
    }
}