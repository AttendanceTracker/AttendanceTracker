using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class Account
    {
        public long ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}