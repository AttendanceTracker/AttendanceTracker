using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class Device:DBMappable
    {
        public long DeviceID { get; set; }
        public long StudentID { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            DeviceID = map.Get<long>("device_id");
            StudentID = map.Get<long>("student_id");
        }
    }
}