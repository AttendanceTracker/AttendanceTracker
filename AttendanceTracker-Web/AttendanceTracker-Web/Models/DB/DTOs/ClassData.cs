using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class ClassData: DBMappable
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long TeacherID { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("id");
            Name = map.Get<string>("name");
            TeacherID = map.Get<long>("teacher_id");
        }
    }
}