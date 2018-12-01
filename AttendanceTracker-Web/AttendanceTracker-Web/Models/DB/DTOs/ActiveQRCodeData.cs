using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB.Mapper;

namespace AttendanceTracker_Web.Models.DB
{
    public class ActiveQRCodeData : DBMappable
    {
        public long ClassID { get; set; }
        public string ClassName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Payload { get; set; }

        public override void MapProperties(DBPropertyMap map)
        {
            ClassID = map.Get<long>("ID");
            ClassName = map.Get<string>("name");
            StartDate = map.Get<DateTime>("start_date");
            EndDate = map.Get<DateTime>("end_date");
            Payload = map.Get<string>("payload");
        }
    }
}