using System;
using System.Configuration;

namespace Lottery.Infrastructure.Extensions
{
    public static class ConfigurationHelper
    {
        public static bool TryGetAppSettingVal(string key, out string val)
        {
            try
            {
                val = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(val))
                {                    
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                val = string.Empty;
                return false;
            }
        }

        public static bool TryGetAppSettingBoolVal(string key, out bool val)
        {
            try
            {
                string valStr = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(valStr))
                {
                    val = Convert.ToBoolean(valStr);
                    return true;
                }
                val = false;
                return false;
            }
            catch (Exception e)
            {
                val = false;
                return false;
            }
        }

        public static bool TryGetAppSettingIntVal(string key, out int val)
        {
            try
            {
                string valStr = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(valStr))
                {
                    val = Convert.ToInt32(valStr);
                    return true;
                }
                val = 0;
                return false;
            }
            catch (Exception e)
            {
                val = 0;
                return false;
            }
        }
    }

}