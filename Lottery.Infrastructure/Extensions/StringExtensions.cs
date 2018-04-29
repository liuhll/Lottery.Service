using Lottery.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lottery.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveUnderLine(this string str)
        {
            return str.Replace("_", "");
        }

        public static string RemoveStrike(this string str)
        {
            return str.Replace("-", "");
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static T ToEnum<T>(this string str) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new LotteryException("类型转换失败,必须为枚举类型");
            }
            var enumValue = (T)(Enum.Parse(typeof(T), str));
            return enumValue;
        }

        public static string ToSplitString(this IEnumerable<string> strList, string split = ",")
        {
            var sb = new StringBuilder();
            foreach (var str in strList)
            {
                sb.Append(str + split);
            }
            return sb.Remove(sb.Length - split.Length, split.Length).ToString();
        }

        public static T ToObject<T>(this string str) where T : class
        {
            if (!str.IsNullOrEmpty())
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            return default(T);
        }

        public static dynamic ToObject(this string str)
        {
            if (!str.IsNullOrEmpty())
            {
                return JsonConvert.DeserializeObject(str);
            }
            return null;
        }

        public static int IndexOfCount(this string str, string keyword, int count)
        {
            int index = 0;
            int initCount = 0;
            while ((index = str.IndexOf(keyword, index)) != -1)
            {
                initCount++;
                index = index + keyword.Length;
                if (initCount == count)
                {
                    break;
                }
            }
            return index;
        }
    }
}