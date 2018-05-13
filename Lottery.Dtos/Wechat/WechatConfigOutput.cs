using System.Collections.Generic;

namespace Lottery.Dtos.Wechat
{
    public class WechatConfigOutput
    {
        public bool Debug { get; set; }

        public string Appid { get; set; }

        public long Timestamp { get; set; }

        public string Signature { get; set; }

        public ICollection<string> JsApiList { get; set; }

    }
}