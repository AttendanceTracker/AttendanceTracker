using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class Attendance: DBMappable
    {
        public long ID { get; set; }
        public long ClassID { get; set; }
        public long StudentID { get; set; }
        public DateTime AttendedDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("id");
            ClassID = map.Get<long>("class_id");
            StudentID = map.Get<long>("attended_student");
            AttendedDate = map.Get<DateTime>("attended_date");
            Latitude = map.Get<decimal>("latitude");
            Longitude = map.Get<decimal>("longitude");
        }
    }
}