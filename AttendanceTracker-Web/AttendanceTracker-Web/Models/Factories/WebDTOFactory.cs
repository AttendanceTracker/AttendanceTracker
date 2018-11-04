using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DTOs.Web;

namespace AttendanceTracker_Web.Models.Factories
{
    public class WebDTOFactory
    {
        public RegisterDeviceRequest RegisterDeviceRequest(string imei, long studentID)
        {
            var dto = new RegisterDeviceRequest();
            dto.imei = imei;
            dto.studentID = studentID;
            return dto;
        }

        public RegisterDeviceResponse RegisterDeviceResponse(string imei, long studentID)
        {
            var dto = new RegisterDeviceResponse();
            dto.imei = imei;
            dto.studentID = studentID;
            return dto;
        }
    }
}