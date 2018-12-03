using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public abstract class DataSource
    {
        public abstract Device AddDevice(Device device);
        public abstract Device UpdateDevice(Device device);
        public abstract Device GetDevice(long imei);
        public abstract void RemoveDevice(long imei);
        public abstract Student AddStudent(Student student);
        public abstract Student UpdateStudent(Student student);
        public abstract Student GetStudent(long cwid);
        public abstract void RemoveStudent(long cwid);
        public abstract Attendance AddAttendance(Attendance attendance);
        public abstract Attendance GetAttendance(long id);
        public abstract List<Attendance> GetAttendance(DateTime date);
        public abstract List<Attendance> GetAttendance(DateTime start, DateTime end);
        public abstract List<Attendance> GetAttendanceByClassID(long classID);
        public abstract QRCode AddQRCode(QRCode qrCode);
        public abstract QRCode GetQRCode(long id);
        public abstract QRCode GetQRCode(long classID, string payload);
        public abstract List<long> GetQRCodes(long classID);
        public abstract AccessToken AddAccessToken(AccessToken accessToken);
        public abstract AccessToken GetAccessToken(string token);
        public abstract void RemoveAccessToken(string token);
        public abstract Account AddAccount(Account account);
        public abstract Account UpdateAccount(Account account);
        public abstract Account GetAccount(string username);
        public abstract Teacher AddTeacher(Teacher teacher);
        public abstract Teacher GetTeacher(long cwid);
        public abstract Teacher GetTeacherByUserID(long userID);
        public abstract List<ClassData> GetClassDataForTeacher(long teacherID);
        public abstract List<ClassData> GetClassDataForStudent(long studentID);
        public abstract List<StudentAttendance> GetStudentAttendance(long studentID, long classID);
        public abstract List<ClassAttendance> GetClassAttendance(long classID, DateTime date);
        public abstract List<ClassAttendance> GetClassAttendance(long classID, DateTime start, DateTime end);
        public abstract List<TeacherAttendance> GetTeacherAttendance(long teacherID, DateTime start, DateTime end);
        public abstract List<TeacherMeetings> GetTeacherMeetings(long teacherID, DateTime start, DateTime end);
        public abstract int GetAttendanceCount(long classID, DateTime date);
        public abstract int GetStudentCountInClass(long classID);
        public abstract List<AttendanceDataPoint> GetTeacherAttendanceData(long teacherID);
        public abstract List<TeacherTotalAttendance> GetTeacherTotalAttendance(long teacherID);
        public abstract List<TeacherTotalMeetings> GetTeacherTotalMeetings(long teacherID);
        public abstract List<TotalAttendanceDataPoints> GetTeacherTotalAttendanceData(long teacherID);
        public abstract List<ActiveQRCodeData> GetActiveQRCodes(long teacherID);
        public abstract List<Class> GetClass(long classID);
    }
}