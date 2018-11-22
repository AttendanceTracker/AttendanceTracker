using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class AccessToken: DBMappable
    {
        public long UserID { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime Issued { get; set; }
        public bool DoesExpire { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            UserID = map.Get<long>("user_id");
            Token = map.Get<string>("token");
            ExpiresIn = map.Get<int>("expires_in");
            Issued = map.Get<DateTime>("issued");
            DoesExpire = map.Get<bool>("does_expire");
        }
    }
}