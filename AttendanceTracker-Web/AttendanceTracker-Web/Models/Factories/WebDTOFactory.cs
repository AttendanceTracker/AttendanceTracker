using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DTOs.Web;

namespace AttendanceTracker_Web.Models.Factories
{
    public class WebDTOFactory
    {
        public RegisterDeviceRequest RegisterDeviceRequest(long imei, long studentID)
        {
            var dto = new RegisterDeviceRequest();
            dto.IMEI = imei;
            dto.StudentID = studentID;
            return dto;
        }

        public RegisterDeviceResponse RegisterDeviceResponse(long imei, long studentID)
        {
            var dto = new RegisterDeviceResponse();
            dto.IMEI = imei;
            dto.StudentID = studentID;
            return dto;
        }

        public RegisterStudentRequest RegisterStudentRequest(long cwid, string firstName, string lastName, string email)
        {
            var dto = new RegisterStudentRequest();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            return dto;
        }

        public RegisterStudentResponse RegisterStudentResponse(long cwid, string firstName, string lastName, string email)
        {
            var dto = new RegisterStudentResponse();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            return dto;
        }
    }
}