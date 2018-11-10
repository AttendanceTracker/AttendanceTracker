using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public class TestDataHelper : DataSource
    {
        DataBaseFactory dbFactory;

        public TestDataHelper()
        {
            dbFactory = new DataBaseFactory();
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
            var resultDevice = dbFactory.Device(imei, imei);
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
            var student = dbFactory.Student(cwid, "Jane", "Doe", "jdoe@a.com");
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

        public override List<Attendance> GetAttendance(DateTime date)
        {
            if (date.Year >= 2018)
            {
                var attendance = GetAttendance(1);
                var list = new List<Attendance>();
                list.Add(attendance);
                return list;
            }
            return null;
        }

        public override List<Attendance> GetAttendance(DateTime start, DateTime end)
        {
            if (start.Year == end.Year)
            {
                var attendance = GetAttendance(1);
                var list = new List<Attendance>();
                list.Add(attendance);
                return list;
            }
            return null;
        }

        public override Attendance GetAttendance(long id)
        {
            if (id == 1) {
                var attendance = dbFactory.Attendance(0, 3, 5, DateTime.Now, 33.216111m, -87.538623m);
                return attendance;
            }
            return null;
        }
    }
}