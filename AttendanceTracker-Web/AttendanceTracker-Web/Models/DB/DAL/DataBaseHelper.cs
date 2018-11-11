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
            var addQueryString = "exec Attendance_AddAttendance @classID, @studentID, @attendedDate, @latitude, @longitude;";
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

        public override List<Attendance> GetAttendance(DateTime date)
        {
            var queryString = "exec Attendance_GetAttendanceByDate @date;";
            var addQuery = new Query(queryString, connectionString);
            addQuery.AddParameter("@date", date);
            var results = addQuery.ExecuteQuery();
            var attendanceResults = BuildAttendanceList(results);
            return attendanceResults;
        }

        public override List<Attendance> GetAttendance(DateTime start, DateTime end)
        {
            var queryString = "exec Attendance_GetAttendanceByDateRange @start_date, @end_date;";
            var addQuery = new Query(queryString, connectionString);
            addQuery.AddParameter("@start_date", start);
            addQuery.AddParameter("@end_date", end);
            var results = addQuery.ExecuteQuery();
            var attendanceResults = BuildAttendanceList(results);
            return attendanceResults;
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

        private List<Attendance> BuildAttendanceList(DataTable table)
        {
            var results = new List<Attendance>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var attendance = BuildAttendance(row);
                results.Add(attendance);
            }
            return results;
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

        public override QRCode AddQRCode(QRCode qrCode)
        {
            var addQueryString = "exec QRCodes_AddQRCode @classID, @payload, @issued, @expires_in;";
            var addQuery = new Query(addQueryString, connectionString);
            addQuery.AddParameter("@classID", qrCode.ClassID);
            addQuery.AddParameter("@payload", qrCode.Payload);
            addQuery.AddParameter("@issued", qrCode.Issued);
            addQuery.AddParameter("@expires_in", qrCode.ExpiresIn);
            var id = (long)addQuery.ExecuteScalar();

            var qrCodeResult = GetQRCode(id);
            return qrCodeResult;
        }

        public override QRCode GetQRCode(long id)
        {
            var queryString = string.Format("exec QRCodes_GetQRCodeByID {0};", id);
            var results = ExecuteStoredProcedure(queryString);
            if (results.Rows.Count != 0)
            {
                var qrCodeResults = BuildQRCode(results.Rows[0]);
                return qrCodeResults;
            }
            return null;
        }

        public override QRCode GetQRCode(long classID, string payload)
        {
            var queryString = "exec QRCodes_GetQRCode @class_id, @payload;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            query.AddParameter("@payload", payload);
            var results = query.ExecuteQuery();
            if (results.Rows.Count != 0)
            {
                var qrCodeResults = BuildQRCode(results.Rows[0]);
                return qrCodeResults;
            }
            return null;
        }

        private QRCode BuildQRCode(DataRow row)
        {
            var idResult = row.Field<long>(0);
            var classIDResult = row.Field<long>(1);
            var payloadResult = row.Field<string>(2);
            var issuedResult = row.Field<DateTime>(3);
            var expiresIn = row.Field<int>(4);
            var resultQRCode = dbFactory.QRCode(idResult, classIDResult, payloadResult, issuedResult, expiresIn);
            return resultQRCode;
        }

        public override AccessToken AddAccessToken(AccessToken accessToken)
        {
            var queryString = "exec Access_Tokens_AddAccess_Token @user_id, @token, @expires_in, @issued, @does_expire;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@user_id", accessToken.UserID);
            query.AddParameter("@token", accessToken.Token);
            query.AddParameter("@expires_in", accessToken.ExpiresIn);
            query.AddParameter("@issued", accessToken.Issued);
            query.AddParameter("@does_expire", accessToken.DoesExpire);
            query.ExecuteQuery();

            var insertedToken = GetAccessToken(accessToken.UserID, accessToken.Token);
            return insertedToken;
        }

        public override AccessToken GetAccessToken(long userID, string token)
        {
            var queryString = "exec Access_Tokens_GetAccess_Token @user_id, @token";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@user_id", userID);
            query.AddParameter("@token", token);
            var results = query.ExecuteQuery();
            if (results.Rows.Count != 0)
            {
                var resultToken = BuildAccessToken(results.Rows[0]);
                return resultToken;
            }
            return null;
        }

        public override void RemoveAccessToken(long userID, string token)
        {
            var queryString = "exec Access_Tokens_RemoveAccess_Token @user_id, @token";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@user_id", userID);
            query.AddParameter("@token", token);
            query.ExecuteQuery();
        }

        private AccessToken BuildAccessToken(DataRow row)
        {
            var userID = row.Field<long>(0);
            var token = row.Field<string>(1);
            var expiresIn = row.Field<int>(2);
            var issued = row.Field<DateTime>(3);
            var doesExpire = row.Field<bool>(4);
            var resultToken = dbFactory.AccessToken(userID, token, expiresIn, issued, doesExpire);
            return resultToken;
        }

        private DataTable ExecuteStoredProcedure(string queryString)
        {
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }
    }
}