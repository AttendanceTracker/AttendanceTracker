using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public abstract class DataSource
    {
        public abstract bool DoesDeviceExist(long imei);
        public abstract Device AddDevice(Device device);
        public abstract Student AddStudent(Student student);
    }
}