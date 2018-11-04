using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DTOs.DB;

namespace AttendanceTracker_Web.Models.Factories
{
    public class DataBaseDTOFactory
    {
        public Device Device(long deviceID, long studentID)
        {
            var dto = new Device();
            dto.DeviceID = deviceID;
            dto.StudentID = studentID;
            return dto;
        }
    }
}