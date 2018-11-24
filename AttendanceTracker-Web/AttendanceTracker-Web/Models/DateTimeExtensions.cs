using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models
{
    public static class DateTimeExtensions
    {
        public static DateTime Round(this DateTime dateTime, TimeSpan span)
        {
            long ticks = (dateTime.Ticks + (span.Ticks / 2) + 1) / span.Ticks;
            var newDate = new DateTime(ticks * span.Ticks);
            return newDate;
        }

        public static DateTime WeekStart(this DateTime dateTime, DayOfWeek StartDay)
        {
            var differenceToStart = (7 + (dateTime.DayOfWeek - StartDay)) % 7;
            var startDate = dateTime.AddDays(-1 * differenceToStart).Date;
            return startDate;
        }
    }
}