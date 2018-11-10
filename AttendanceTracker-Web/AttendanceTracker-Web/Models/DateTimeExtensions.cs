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
    }
}