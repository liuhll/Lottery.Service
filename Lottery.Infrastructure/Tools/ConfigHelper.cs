using System;
using System.Configuration;

namespace Lottery.Infrastructure.Tools
{
    public static class ConfigHelper
    {
        public static int ValueInt(string key)
        {
            return Convert.ToInt32(Value(key));
        }

        public static string Value(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            return val;
        }

        //public static object ValueString(string key)
        //{
        //    var val = ConfigurationManager.AppSettings[key];
        //    return val.ToString();
        //}
    }
}