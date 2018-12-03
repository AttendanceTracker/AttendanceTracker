using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class Class: DBMappable
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentEmail { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("class_id");
            StudentID = map.Get<long>("cwid");
            StudentFirstName = map.Get<string>("first_name");
            StudentLastName = map.Get<string>("last_name");
            StudentEmail = map.Get<string>("email");
        }
    }
}