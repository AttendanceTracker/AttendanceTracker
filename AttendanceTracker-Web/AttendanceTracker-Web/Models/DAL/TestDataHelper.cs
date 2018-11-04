using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.Factories;

namespace AttendanceTracker_Web.Models.DAL
{
    public class TestDataHelper : DataSource
    {
        public override bool DoesDeviceExist(string imei)
        {
            return true;
        }

        public override void AddDevice(string imei, long studentID)
        {
        }
    }
}