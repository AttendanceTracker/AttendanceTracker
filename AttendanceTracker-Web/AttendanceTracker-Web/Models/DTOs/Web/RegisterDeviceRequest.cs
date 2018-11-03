using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DTOs.Web
{
    public class RegisterDeviceRequest
    {
        public string imei { get; set; }
        public long studentID { get; set; }
    }
}