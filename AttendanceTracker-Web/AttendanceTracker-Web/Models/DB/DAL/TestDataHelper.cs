using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class TestDataHelper : DataSource
    {
        DataBaseFactory dbDTOFactory;

        public TestDataHelper()
        {
            dbDTOFactory = new DataBaseFactory();
        }

        public override bool DoesDeviceExist(long imei)
        {
            if (imei < 2)
            {
                return true;
            }
            return false;
        }

        public override Device AddDevice(Device device)
        {
            var resultDevice = device;
            return resultDevice;
        }

        public override Student AddStudent(Student student)
        {
            var resultStudent = student;
            return resultStudent;
        }
    }
}