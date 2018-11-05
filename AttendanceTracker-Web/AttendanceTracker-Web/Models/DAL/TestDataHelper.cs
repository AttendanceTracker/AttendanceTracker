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