using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DTOs.DB;
using AttendanceTracker_Web.Models.Factories;

namespace AttendanceTracker_Web.Models.DAL
{
    public class TestDataHelper : DataSource
    {
        DataBaseDTOFactory dbDTOFactory;

        public TestDataHelper()
        {
            dbDTOFactory = new DataBaseDTOFactory();
        }

        public override bool DoesDeviceExist(long imei)
        {
            return true;
        }

        public override Device AddDevice(long imei, long studentID)
        {
            var deviceID = imei;
            var CWID = studentID;
            var device = dbDTOFactory.Device(imei, studentID);
            return device;
        }
    }
}