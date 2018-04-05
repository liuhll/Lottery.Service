using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using Lottery.Dtos.OnlineHelp;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.OnlineHelps;

namespace Lottery.AppService.Operations
{
    [Component]
    public class OnlineHelpAppService : IOnlineHelpAppService
    {
        private readonly IOnlineHelpQueryService _onlineHelpQueryService;

        public OnlineHelpAppService(IOnlineHelpQueryService onlineHelpQueryService)
        {
            _onlineHelpQueryService = onlineHelpQueryService;
        }

        public ICollection<OnlineGroupOutput> GetOnlineHelps(string lotteryCode)
        {
            var result = new List<OnlineGroupOutput>();
            var helps = _onlineHelpQueryService.GetOnlineHelps(lotteryCode);

            var helpGroups = helps.GroupBy(p => p.HelpType);
            foreach (var helpGroup in helpGroups)
            {
                var onlineGroup = new OnlineGroupOutput()
                {
                    HelpType = (OnlineHelpType)helpGroup.Key
                };
                onlineGroup.OnlineItems = new List<OnlineItemOutput>();
                foreach (var item in helpGroup)
                {
                    var onlineHelpItem = new OnlineItemOutput()
                    {
                        Content = item.Content,
                        Title = item.Title
                    };
                    onlineGroup.OnlineItems.Add(onlineHelpItem);
                }
                result.Add(onlineGroup);
            }
            return result;
        }
    }
}