using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class TeacherTotalAttendance: DBMappable
    {
        public string ClassName { get; set; }
        public int AttendanceCount { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ClassName = map.Get<string>("name");
            AttendanceCount = map.Get<int>("attended_count");
        }
    }
}