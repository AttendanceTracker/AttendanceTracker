using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class QRCode: DBMappable
    {
        public long ID { get; set; }
        public long ClassID { get; set; }
        public string Payload { get; set; }
        public DateTime Issued { get; set; }
        public int ExpiresIn { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ID = map.Get<long>("id");
            ClassID = map.Get<long>("class_id");
            Payload = map.Get<string>("payload");
            Issued = map.Get<DateTime>("issued");
            ExpiresIn = map.Get<int>("expires_in");
        }
    }
}