using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DAL
{
    public class DataBaseHelper : DataSource
    {
        public override bool DoesDeviceExist(string imei)
        {
            throw new NotImplementedException();
        }

        public override void AddDevice(string imei, long studentID)
        {
            throw new NotImplementedException();
        }
    }
}