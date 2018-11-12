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

        public override QRCode AddQRCode(QRCode qrCode)
        {
            var resultQRCode = qrCode;
            return resultQRCode;
        }

        public override QRCode GetQRCode(long id)
        {
            if (id == 1)
            {
                var qrCode = dbFactory.QRCode(id, 3, "testasdf", DateTime.Now, 1000);
                return qrCode;
            }
            return null;
        }

        public override QRCode GetQRCode(long classID, string payload)
        {
            if (payload != "wrong")
            {
                var qrCode = dbFactory.QRCode(0, 3, payload, DateTime.Now, 10000);
                return qrCode;
            }
            return null;
        }

        public override AccessToken AddAccessToken(AccessToken accessToken)
        {
            var token = accessToken;
            return token;
        }

        public override AccessToken GetAccessToken(string token)
        {
            if (token != "bad")
            {
                var accessToken = dbFactory.AccessToken(1, token, 1, DateTime.Now, true);
                return accessToken;
            }
            return null;
        }

        public override void RemoveAccessToken(string token)
        {
            //remove token
        }

        public override Account AddAccount(Account account)
        {
            var newAccount = account;
            return newAccount;
        }

        public override Account UpdateAccount(Account account)
        {
            var updatedAccount = account;
            return updatedAccount;
        }

        public override Account GetAccount(string username)
        {
            if(username == "jdoe@a.com")
            {
                var account = dbFactory.Account(0, username, "asdf", "asdf");
                return account;
            }
            return null;
        }

        public override Teacher AddTeacher(Teacher teacher)
        {
            var newTeacher = teacher;
            return newTeacher;
        }

        public override Teacher GetTeacher(long cwid)
        {
            if (cwid == 1)
            {
                var teacher = dbFactory.Teacher(cwid, 1, "john", "doe", "jdoe@a.com");
                return teacher;
            }
            return null;
        }

        public override Teacher GetTeacherByUserID(long userID)
        {
            if (userID == 1)
            {
                var teacher = dbFactory.Teacher(1, userID, "john", "doe", "jdoe@a.com");
                return teacher;
            }
            return null;
        }
    }
}