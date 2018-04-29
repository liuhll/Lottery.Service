using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using System.Collections.Generic;

namespace Lottery.Dtos.OnlineHelp
{
    public class OnlineGroupOutput
    {
        public OnlineHelpType HelpType { get; set; }

        public string HelpTypeDesc
        {
            get { return HelpType.GetChineseDescribe(); }
        }

        public ICollection<OnlineItemOutput> OnlineItems { get; set; }
    }
}