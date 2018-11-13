using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class ClassData
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long TeacherID { get; set; }
    }
}