using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class Attendance
    {
        public long id { get; set; }
        public long ClassID { get; set; }
        public long StudentID { get; set; }
        public DateTime attendedDate { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}