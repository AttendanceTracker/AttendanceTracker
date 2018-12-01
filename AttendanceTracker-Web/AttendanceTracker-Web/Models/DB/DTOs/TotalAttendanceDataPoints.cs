using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class TotalAttendanceDataPoints: DBMappable
    {
        public DateTime MeetingTime { get; set; }
        public int StudentCount { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            MeetingTime = map.Get<DateTime>("issued");
            StudentCount = map.Get<int>("student_count");
        }
    }
}