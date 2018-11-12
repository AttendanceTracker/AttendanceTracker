using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.Web
{
    public class QRCodePayload
    {
        public long ClassID { get; set; }
        public string Payload { get; set; }
    }
}