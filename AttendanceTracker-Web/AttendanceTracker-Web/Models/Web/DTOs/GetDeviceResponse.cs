﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.Web
{
    public class GetDeviceResponse
    {
        public long IMEI { get; set; }
        public long StudentID { get; set; }
    }
}