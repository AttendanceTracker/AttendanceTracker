using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DAL
{
    public abstract class DataSource
    {
        public abstract bool DoesDeviceExist(string imei);
        public abstract void AddDevice(string imei, long studentID);
    }
}