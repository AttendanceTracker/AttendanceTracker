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

        public override Device AddDevice(Device device)
        {
            var resultDevice = device;
            return resultDevice;
        }

        public override Device UpdateDevice(Device device)
        {
            var resultDevice = device;
            return resultDevice;
        }

        public override Device GetDevice(long imei)
        {
            var resultDevice = dbDTOFactory.Device(imei, imei);
            return resultDevice;
        }

        public override void RemoveDevice(long imei)
        {
            // remove device with imei
        }

        public override Student AddStudent(Student student)
        {
            var resultStudent = student;
            return resultStudent;
        }

        public override Student UpdateStudent(Student student)
        {
            var resultStudent = student;
            return student;
        }

        public override Student GetStudent(long cwid)
        {
            var student = dbDTOFactory.Student(cwid, "Jane", "Doe", "jdoe@a.com");
            return student;
        }

        public override void RemoveStudent(long cwid)
        {
            // remove student with cwid
        }

        public override Attendance AddAttendance(Attendance attendance)
        {
            var resultAttendance = attendance;
            return resultAttendance;
        }

        public override Attendance GetAttendance(long id)
        {
            throw new NotImplementedException();
        }

        public override Attendance GetAttendance(DateTime date)
        {
            throw new NotImplementedException();
        }

        public override Attendance GetAttendance(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}