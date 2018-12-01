using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class TeacherTotalMeetings: DBMappable
    {
        public string ClassName { get; set; }
        public int ClassMeetingCount { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ClassName = map.Get<string>("name");
            ClassMeetingCount = map.Get<int>("meeting_count");
        }
    }
}