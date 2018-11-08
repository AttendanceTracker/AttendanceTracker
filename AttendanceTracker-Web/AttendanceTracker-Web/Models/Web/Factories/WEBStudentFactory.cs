using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.Web
{
    public class WebStudentFactory: WebFactory
    {
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