using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class DataBaseFactory
    {
        public Device Device(long deviceID, long studentID)
        {
            var dto = new Device();
            dto.DeviceID = deviceID;
            dto.StudentID = studentID;
            return dto;
        }

        public Student Student(long cwid, string firstName, string lastName, string email)
        {
            var dto = new Student();
            dto.CWID = cwid;
            dto.FirstName = firstName;
            dto.LastName = lastName;
            dto.Email = email;
            return dto;
        }

        public Attendance Attendance(long id, long classID, long studentID, DateTime attendedDate, double latitude, double longitude)
        {
            var dto = new Attendance();
            dto.id = id;
            dto.ClassID = classID;
            dto.StudentID = studentID;
            dto.attendedDate = attendedDate;
            dto.latitude = latitude;
            dto.longitude = longitude;
            return dto;
        }
    }
}