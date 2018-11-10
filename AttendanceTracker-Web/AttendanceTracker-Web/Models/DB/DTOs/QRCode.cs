using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class QRCode
    {
        public long ID { get; set; }
        public long ClassID { get; set; }
        public string Payload { get; set; }
        public DateTime Issued { get; set; }
        public int ExpiresIn { get; set; }
    }
}