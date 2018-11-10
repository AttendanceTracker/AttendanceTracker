using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.Web
{
    public class CheckInRequest
    {
        public long StudentID { get; set; }
        public string Payload { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}