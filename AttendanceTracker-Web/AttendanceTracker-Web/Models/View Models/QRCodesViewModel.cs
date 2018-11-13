using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Models
{
    public class QRCodesViewModel
    {
        public List<ClassData> ClassData { get; set; }
        public List<long> QRCodes { get; set; }
    }
}