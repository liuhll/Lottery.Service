using System;

namespace Lottery.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        //public static bool IsBetween(this DateTime dateTime, TimeSpan startTime, TimeSpan endTime)
        //{
        //    var now = DateTime.Now.TimeOfDay;
        //    if ( now > startTime && now < endTime)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public static DateTime StartTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0); ;
        }

        public static DateTime TimeStampConvetDateTime(long timestamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(timestamp);
            return dt;
        }


    }
}