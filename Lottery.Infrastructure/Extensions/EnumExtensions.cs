using System;
using System.Linq;
using System.Reflection;
using Lottery.Infrastructure.Attributes;

namespace Lottery.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetChineseDescribe(this Enum enumval)
        {
            var descAttrs = enumval.GetType().GetCustomAttributes<EnumDescribeAttribute>();
            var descAttr = descAttrs.FirstOrDefault();
            if (descAttr != null)
            {
                return descAttr.Describe;
            }
            return String.Empty;
        }
    }
}