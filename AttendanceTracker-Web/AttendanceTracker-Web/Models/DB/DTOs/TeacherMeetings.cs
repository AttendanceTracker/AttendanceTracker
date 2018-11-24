using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class TeacherMeetings : DBMappable
    {
        public long ID { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Name { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("id");
            MeetingDate = map.Get<DateTime>("issued");
            Name = map.Get<string>("name");
        }
    }
}