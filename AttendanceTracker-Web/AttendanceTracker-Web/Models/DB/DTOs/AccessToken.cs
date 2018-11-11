using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class AccessToken
    {
        public long UserID { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime Issued { get; set; }
        public bool DoesExpire { get; set; }
    }
}