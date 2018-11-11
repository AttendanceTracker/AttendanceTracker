using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.Web
{
    public class WebFactory
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

        public GetDeviceResponse GetDeviceResponse(long imei, long studentID)
        {
            var dto = new GetDeviceResponse();
            dto.IMEI = imei;
            dto.StudentID = studentID;
            return dto;
        }

        public UpdateDeviceRequest UpdateDeviceRequest(long studentID)
        {
            var dto = new UpdateDeviceRequest();
            dto.StudentID = studentID;
            return dto;
        }

        public UpdateDeviceResponse UpdateDeviceResponse(long imei, long studentID)
        {
            var dto = new UpdateDeviceResponse();
            dto.IMEI = imei;
            dto.StudentID = studentID;
            return dto;
        }

        public GetStudentResponse GetStudentResponse(long cwid, string firstName, string lastName, string email)
        {
            var dto = new GetStudentResponse();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
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

        public UpdateStudentRequest UpdateStudentRequest(string firstName, string lastName, string email)
        {
            var dto = new UpdateStudentRequest();
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            return dto;
        }

        public UpdateStudentResponse UpdateStudentResponse(long cwid, string firstName, string lastName, string email)
        {
            var dto = new UpdateStudentResponse();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            return dto;
        }

        public CheckInRequest CheckInRequest(long studentID, string payload, decimal latitude, decimal longitude)
        {
            var dto = new CheckInRequest();
            dto.StudentID = studentID;
            dto.Payload = payload;
            dto.Latitude = latitude;
            dto.Longitude = longitude;
            return dto;
        }

        public CreateAccountRequest CreateAccountRequest(long cwid, string firstName, string lastName, string email, string password)
        {
            var dto = new CreateAccountRequest();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            dto.Password = password;
            return dto;
        }
    }
}