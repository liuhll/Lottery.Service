using System.Collections.Generic;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Dtos.OnlineHelp
{
    public class OnlineGroupOutput
    {
        public OnlineHelpType HelpType { get; set; }

        public string HelpTypeDesc {
            get { return HelpType.GetChineseDescribe(); }
        }

        public ICollection<OnlineItemOutput> OnlineItems { get; set; }

    }
}