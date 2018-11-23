using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class StudentAttendance : DBMappable
    {
        public DateTime Date { get; set; }
        public bool DidAttend { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            Date = map.Get<DateTime>("date");
            DidAttend = map.Get<bool>("did_attend");
        }
    }
}