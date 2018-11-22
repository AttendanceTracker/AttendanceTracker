using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class Account: DBMappable
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("id");
            Username = map.Get<string>("username");
            Password = map.Get<string>("password");
            Salt = map.Get<string>("salt");
        }
    }


}