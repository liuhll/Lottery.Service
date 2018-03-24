using System;
using System.Collections.Generic;
using System.IO;
using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.Mail
{
    public static class EmailTempletHelper
    {
        public static string ReadContent(string templetName,IDictionary<string,string> templetParams, EmailTempletType templetType = EmailTempletType.Text)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var emailTempletName = "";
            switch (templetType)
            {
                case EmailTempletType.Text:
                    emailTempletName = templetName + ".txt";
                    break;
                case EmailTempletType.Html:
                    emailTempletName = templetName + ".html";
                    break;
            }
            var emailTempletPath = Path.Combine(baseDir, "bin/Mail/Templet", emailTempletName);
            var content = File.ReadAllText(emailTempletPath);
            //content = content.Replace("\r", " ");
            //content = content.Replace("\n", " ");
            if (templetParams != null && templetParams.Count > 0)
            {
                foreach (var param in templetParams)
                {
                    content = content.Replace("${" + param.Key +"}", param.Value);
                }
            }
            return content;
        }
    }
}
