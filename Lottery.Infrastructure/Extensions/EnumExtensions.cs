using Lottery.Infrastructure.Attributes;
using System;
using System.Linq;

namespace Lottery.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetChineseDescribe(this Enum enumval)
        {
            var type = enumval.GetType();
            var fieldName = Enum.GetName(type, enumval);
            var attributes = type.GetField(fieldName).GetCustomAttributes(false);
            var enumDisplayAttribute = attributes.FirstOrDefault(p => p.GetType().Equals(typeof(EnumDescribeAttribute))) as EnumDescribeAttribute;
            return enumDisplayAttribute == null ? fieldName : enumDisplayAttribute.Describe;
        }
    }
}