﻿using System;
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

        public override List<Attendance> GetAttendanceByClassID(long classID)
        {
            if (classID == 1)
            {
                var attendance = GetAttendance(1);
                var list = new List<Attendance>();
                list.Add(attendance);
                return list;
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

        public override List<long> GetQRCodes(long classID)
        {
            var qrCodeIDs = new List<long>();
            if (classID == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    qrCodeIDs.Add(i);
                }
            }
            return qrCodeIDs;
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

        public override List<ClassData> GetClassDataForTeacher(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<ClassData> GetClassDataForStudent(long studentID)
        {
            throw new NotImplementedException();
        }

        public override List<StudentAttendance> GetStudentAttendance(long studentID, long classID)
        {
            throw new NotImplementedException();
        }

        public override List<ClassAttendance> GetClassAttendance(long classID, DateTime date)
        {
            throw new NotImplementedException();
        }

        public override List<ClassAttendance> GetClassAttendance(long classID, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public override List<TeacherAttendance> GetTeacherAttendance(long teacherID, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public override List<TeacherMeetings> GetTeacherMeetings(long teacherID, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public override int GetAttendanceCount(long classID, DateTime date)
        {
            throw new NotImplementedException();
        }

        public override int GetStudentCountInClass(long classID)
        {
            throw new NotImplementedException();
        }

        public override List<AttendanceDataPoint> GetTeacherAttendanceData(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<TeacherTotalAttendance> GetTeacherTotalAttendance(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<TeacherTotalMeetings> GetTeacherTotalMeetings(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<TotalAttendanceDataPoints> GetTeacherTotalAttendanceData(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<ActiveQRCodeData> GetActiveQRCodes(long teacherID)
        {
            throw new NotImplementedException();
        }

        public override List<ClassStudents> GetClass(long classID)
        {
            throw new NotImplementedException();
        }

        public override QRCode GetQRCode(string payload)
        {
            throw new NotImplementedException();
        }

        public override void RemoveStudentFromClass(long classID, long studentID)
        {
            throw new NotImplementedException();
        }

        public override void AddStudentToClass(long classID, long studentID)
        {
            throw new NotImplementedException();
        }

        public override long AddClassData(ClassData classData)
        {
            throw new NotImplementedException();
        }

        public override void RemoveClass(long classID)
        {
            throw new NotImplementedException();
        }

        public override void RemoveQRCode(long qrCodeID)
        {
            throw new NotImplementedException();
        }
    }
}