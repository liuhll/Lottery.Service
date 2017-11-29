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
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
        }

    
    }
}