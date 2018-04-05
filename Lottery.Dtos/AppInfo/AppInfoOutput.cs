using System;

namespace Lottery.Dtos.AppInfo
{
    public class AppInfoOutput
    {
        public string AppName { get; set; }

        public int AppVersion { get; set; }

        public string VersionName { get; set; }

        public string AppAddress { get; set; }

        public string Memo { get; set; }
    }
}
