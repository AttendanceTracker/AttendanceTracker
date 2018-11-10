using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AttendanceTracker_Web.Models.DB
{
    public class DataBaseHelper : DataSource
    {
        string connectionString;
        DataBaseFactory dbFactory;

        public DataBaseHelper()
        {
            Init("Default");
        }

        public DataBaseHelper(string connectionSource)
        {
            Init(connectionSource);
        }

        private void Init(string connectionSource)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionSource].ConnectionString;
            dbFactory = new DataBaseFactory();
        }

        public override Device AddDevice(Device device)
        {
            var queryString = string.Format("exec Devices_AddDevice {0}, {1};", device.DeviceID, device.StudentID);
            ExecuteStoredProcedure(queryString);

            var resultDevice = GetDevice(device.DeviceID);
            return resultDevice;
        }

        public override Device UpdateDevice(Device device)
        {
            var queryString = string.Format("exec Devices_UpdateDevice {0}, {1}", device.DeviceID, device.StudentID);
            ExecuteStoredProcedure(queryString);

            var resultDevice = GetDevice(device.DeviceID);
            return resultDevice;
        }

        public override Device GetDevice(long imei)
        {
            var results = GetDeviceData(imei);
            if (results.Rows.Count != 0)
            {
                var data = results.Rows[0];
                var imeiResult = data.Field<long>(0);
                var resultStudentID = data.Field<long>(1);
                var device = dbFactory.Device(imeiResult, resultStudentID);
                return device;
            }
            return null;
        }

        private DataTable GetDeviceData(long imei)
        {
            var queryString = string.Format("exec Devices_GetDevice {0};", imei);
            var results = ExecuteStoredProcedure(queryString);
            return results;
        }

        public override void RemoveDevice(long imei)
        {
            var queryString = string.Format("exec Devices_RemoveDevice {0}", imei);
            ExecuteStoredProcedure(queryString);
        }

        public override Student AddStudent(Student student)
        {
            var queryString = string.Format("exec Students_AddStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            ExecuteStoredProcedure(queryString);

            var resultStudent = GetStudent(student.CWID);
            return resultStudent;
        }

        public override Student UpdateStudent(Student student)
        {
            var queryString = string.Format("exec Students_UpdateStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            ExecuteStoredProcedure(queryString);

            var resultStudent = GetStudent(student.CWID);
            return resultStudent;
        }

        public override Student GetStudent(long cwid)
        {
            var results = GetStudentData(cwid);
            if (results.Rows.Count != 0)
            {
                var studentData = results.Rows[0];
                var cwidResult = studentData.Field<long>(0);
                var firstName = studentData.Field<string>(1);
                var lastName = studentData.Field<string>(2);
                var email = studentData.Field<string>(3);
                var resultStudent = dbFactory.Student(cwidResult, firstName, lastName, email);
                return resultStudent;
            }
            return null;
        }

        private DataTable GetStudentData(long cwid)
        {
            var queryString = string.Format("exec Students_GetStudent {0};", cwid);
            var results = ExecuteStoredProcedure(queryString);
            return results;
        }

        public override void RemoveStudent(long cwid)
        {
            var queryString = string.Format("exec Students_RemoveStudent {0};", cwid);
            ExecuteStoredProcedure(queryString);
        }

        public override Attendance AddAttendance(Attendance attendance)
        {
            var addQueryString = "exec Attendance_AddAttendance @classID, @studentID, @attendedDate, @latitude, @longitude";
            var addQuery = new Query(addQueryString, connectionString);
            addQuery.AddParameter("@classID", attendance.ClassID);
            addQuery.AddParameter("@studentID", attendance.StudentID);
            addQuery.AddParameter("@attendedDate", attendance.attendedDate);
            addQuery.AddParameter("@latitude", attendance.latitude);
            addQuery.AddParameter("@longitude", attendance.longitude);
            var id = (long)addQuery.ExecuteScalar();

            var getQueryString = string.Format("exec Attendance_GetAttendanceByID {0};", id);
            var result = GetAttendance(getQueryString);
            return result;
        }

        public override Attendance GetAttendance(long id)
        {
            var queryString = string.Format("exec Attendance_GetAttendanceByID {0};", id);
            var result = GetAttendance(queryString);
            return result;
        }

        public override Attendance GetAttendance(DateTime date)
        {
            var queryString = string.Format("exec Attendance_GetAttendanceByDateRange {0};", date);
            var result = GetAttendance(queryString);
            return result;
        }

        public override Attendance GetAttendance(DateTime start, DateTime end)
        {
            var queryString = string.Format("exec Attendance_GetAttendanceByDateRange {0}, {1};", start, end);
            var result = GetAttendance(queryString);
            return result;
        }

        private Attendance GetAttendance(string queryString)
        {
            var results = ExecuteStoredProcedure(queryString);
            if (results.Rows.Count != 0)
            {
                var attendanceData = results.Rows[0];
                var resultAttendance = BuildAttendance(attendanceData);
                return resultAttendance;
            }
            return null;
        }

        private Attendance BuildAttendance(DataRow row)
        {
            var idResult = row.Field<long>(0);
            var classIDResult = row.Field<long>(1);
            var studentIDResult = row.Field<long>(2);
            var attendedDateResult = row.Field<DateTime>(3);
            var latitudeResult = row.Field<decimal>(4);
            var longitudeResult = row.Field<decimal>(5);
            var resultAttendance = dbFactory.Attendance(idResult, classIDResult, studentIDResult, attendedDateResult, latitudeResult, longitudeResult);
            return resultAttendance;
        }

        private DataTable ExecuteStoredProcedure(string queryString)
        {
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }
    }
}