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
        public abstract QRCode AddQRCode(QRCode qrCode);
        public abstract QRCode GetQRCode(long id);
        public abstract QRCode GetQRCode(long classID, string payload);
        public abstract AccessToken AddAccessToken(AccessToken accessToken);
        public abstract AccessToken GetAccessToken(long userID, string token);
        public abstract void RemoveAccessToken(long userID, string token);
    }
}