using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AttendanceTracker_Web.Models.DB.Mapper;

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
            var queryString = "exec Devices_GetDevice @device_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@device_id", imei);
            var results = query.Execute();
            var deviceResults = results.Rows[0].ToDTO<Device>();
            return deviceResults;
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
            var queryString = "exec Students_GetStudent @student_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@student_id", cwid);
            var results = query.Execute();
            var studentResults = results.Rows[0].ToDTO<Student>();
            return studentResults;
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
            addQuery.AddParameter("@attendedDate", attendance.AttendedDate);
            addQuery.AddParameter("@latitude", attendance.Latitude);
            addQuery.AddParameter("@longitude", attendance.Longitude);
            var id = (long)addQuery.ExecuteScalar();
            
            var result = GetAttendance(id);
            return result;
        }

        public override Attendance GetAttendance(long id)
        {
            var queryString = "exec Attendance_GetAttendanceByID @id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@id", id);
            var results = query.Execute();
            var attendanceResults = results.Rows.ToDTOList<Attendance>();
            if (attendanceResults.Count > 0)
            {
                return attendanceResults[0];
            }
            return null;
        }

        public override List<Attendance> GetAttendance(DateTime date)
        {
            var queryString = "exec Attendance_GetAttendanceByDate @date;";
            var addQuery = new Query(queryString, connectionString);
            addQuery.AddParameter("@date", date);
            var results = addQuery.Execute();
            var attendanceResults = results.Rows.ToDTOList<Attendance>();
            return attendanceResults;
        }

        public override List<Attendance> GetAttendance(DateTime start, DateTime end)
        {
            var queryString = "exec Attendance_GetAttendanceByDateRange @start_date, @end_date;";
            var addQuery = new Query(queryString, connectionString);
            addQuery.AddParameter("@start_date", start);
            addQuery.AddParameter("@end_date", end);
            var results = addQuery.Execute();
            var attendanceResults = results.Rows.ToDTOList<Attendance>();
            return attendanceResults;
        }

        public override List<Attendance> GetAttendanceByClassID(long classID)
        {
            var queryString = "exec Attendance_GetAttendanceByClassID @class_id";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            var results = query.Execute();
            var attendanceList = results.Rows.ToDTOList<Attendance>();
            return attendanceList;
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
                var qrCodeResults = results.Rows[0].ToDTO<QRCode>();
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
            var results = query.Execute();
            if (results.Rows.Count != 0)
            {
                var qrCodeResults = results.Rows[0].ToDTO<QRCode>();
                return qrCodeResults;
            }
            return null;
        }

        public override List<long> GetQRCodes(long classID)
        {
            var queryString = "exec QRCodes_GetForClassID @class_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            var results = query.Execute();
            var qrCodeIDs = new List<long>();
            for (int i = 0; i < results.Rows.Count; i++)
            {
                var row = results.Rows[i];
                var id = row.Field<long>(0);
                qrCodeIDs.Add(id);
            }
            return qrCodeIDs;
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
            query.Execute();

            var insertedToken = GetAccessToken(accessToken.Token);
            return insertedToken;
        }

        public override AccessToken GetAccessToken(string token)
        {
            var queryString = "exec Access_Tokens_GetAccess_Token @token";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@token", token);
            var results = query.Execute();
            if (results.Rows.Count != 0)
            {
                var resultToken = results.Rows[0].ToDTO<AccessToken>();
                return resultToken;
            }
            return null;
        }

        public override void RemoveAccessToken(string token)
        {
            var queryString = "exec Access_Tokens_RemoveAccess_Token @token";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@token", token);
            query.Execute();
        }

        public override Account AddAccount(Account account)
        {
            var queryString = "exec Accounts_AddAccount @username, @password, @salt;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@username", account.Username);
            query.AddParameter("@password", account.Password);
            query.AddParameter("@salt", account.Salt);
            query.Execute();

            var resultAccount = GetAccount(account.Username);
            return resultAccount;
        }

        public override Account UpdateAccount(Account account)
        {
            var queryString = "exec Accounts_UpdateAccount @username, @password, @salt;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@username", account.Username);
            query.AddParameter("@password", account.Password);
            query.AddParameter("@salt", account.Salt);
            query.Execute();

            var resultAccount = GetAccount(account.Username);
            return resultAccount;
        }

        public override Account GetAccount(string username)
        {
            var queryString = "exec Accounts_GetAccount @username;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@username", username);
            var results = query.Execute();
            if (results.Rows.Count != 0)
            {
                var resultAccount = results.Rows[0].ToDTO<Account>();
                return resultAccount;
            }
            return null;
        }

        public override Teacher AddTeacher(Teacher teacher)
        {
            var queryString = "exec Teachers_AddTeacher @cwid, @user_id, @first_name, @last_name, @email;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@cwid", teacher.CWID);
            query.AddParameter("@user_id", teacher.UserID);
            query.AddParameter("@first_name", teacher.FirstName);
            query.AddParameter("@last_name", teacher.LastName);
            query.AddParameter("@email", teacher.Email);
            query.Execute();

            var resultTeacher = GetTeacher(teacher.CWID);
            return resultTeacher;
        }

        public override Teacher GetTeacher(long cwid)
        {
            var queryString = "exec Teachers_GetTeacher @cwid;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@cwid", cwid);
            var results = query.Execute();
            if (results.Rows.Count != 0)
            {
                var resultTeacher = results.Rows[0].ToDTO<Teacher>();
                return resultTeacher;
            }
            return null;
        }

        public override Teacher GetTeacherByUserID(long userID)
        {
            var queryString = "exec Teachers_GetTeacherByUserID @user_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@user_id", userID);
            var results = query.Execute();
            if (results.Rows.Count != 0)
            {
                var resultTeacher = results.Rows[0].ToDTO<Teacher>();
                return resultTeacher;
            }
            return null;
        }

        public override List<ClassData> GetClassDataForTeacher(long teacherID)
        {
            var queryString = "exec Class_Data_GetForTeacherID @teacher_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            var results = query.Execute();
            var classDataList = results.Rows.ToDTOList<ClassData>();
            return classDataList;
        }

        public override List<ClassData> GetClassDataForStudent(long studentID)
        {
            var queryString = "exec Class_Data_GetForStudentID @student_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@student_id", studentID);
            var results = query.Execute();
            var classDataList = results.Rows.ToDTOList<ClassData>();
            return classDataList;
        }

        public override List<StudentAttendance> GetStudentAttendance(long studentID, long classID)
        {
            var queryString = "exec GetAttendanceForStudentByClass @student_id, @class_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@student_id", studentID);
            query.AddParameter("@class_id", classID);
            var results = query.Execute();
            var studentAttendanceResults = results.Rows.ToDTOList<StudentAttendance>();
            return studentAttendanceResults;
        }

        public override List<ClassAttendance> GetClassAttendance(long classID, DateTime date)
        {
            var queryString = "exec GetAttendanceForClassByDate @class_id, @date;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            query.AddParameter("@date", date);
            var results = query.Execute();
            var classAttendanceResults = results.Rows.ToDTOList<ClassAttendance>();
            return classAttendanceResults;
        }

        public override List<ClassAttendance> GetClassAttendance(long classID, DateTime start, DateTime end)
        {
            var queryString = "exec GetAttendanceForClassByDateRange @class_id, @start_date, @end_date;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            query.AddParameter("@start_date", start);
            query.AddParameter("@end_date", end);
            var results = query.Execute();
            var classAttendanceResults = results.Rows.ToDTOList<ClassAttendance>();
            return classAttendanceResults;
        }

        public override List<TeacherAttendance> GetTeacherAttendance(long teacherID, DateTime start, DateTime end)
        {
            var queryString = "exec GetAttendanceForTeacherByDateRange @teacher_id, @start_date, @end_date;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            query.AddParameter("@start_date", start);
            query.AddParameter("@end_date", end);
            var results = query.Execute();
            var teacherAttendanceResults = results.Rows.ToDTOList<TeacherAttendance>();
            return teacherAttendanceResults;
        }

        public override List<TeacherMeetings> GetTeacherMeetings(long teacherID, DateTime start, DateTime end)
        {
            var queryString = "exec GetclassMeetingsForTeacherByDateRange @teacher_id, @start_date, @end_date;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            query.AddParameter("@start_date", start);
            query.AddParameter("@end_date", end);
            var results = query.Execute();
            var classResults = results.Rows.ToDTOList<TeacherMeetings>();
            return classResults;
        }

        public override int GetAttendanceCount(long classID, DateTime date)
        {
            var queryString = "exec CountAttendanceForClassByDate @class_id, @date;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            query.AddParameter("@date", date);
            var results = (int) query.ExecuteScalar();
            return results;
        }

        public override int GetStudentCountInClass(long classID)
        {
            var queryString = "exec CountStudentsForClass @class_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@class_id", classID);
            var results = (int)query.ExecuteScalar();
            return results;
        }

        public override List<AttendanceDataPoint> GetTeacherAttendanceData(long teacherID)
        {
            var queryString = "exec GetAttendanceForTeacher @teacher_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            var results = query.Execute();
            var attendanceData = results.Rows.ToDTOList<AttendanceDataPoint>();
            return attendanceData;
        }

        public override List<TeacherTotalAttendance> GetTeacherTotalAttendance(long teacherID)
        {
            var queryString = "exec CountTotalAttendanceForTeacher @teacher_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            var results = query.Execute();
            var attendanceData = results.Rows.ToDTOList<TeacherTotalAttendance>();
            return attendanceData;
        }

        public override List<TeacherTotalMeetings> GetTeacherTotalMeetings(long teacherID)
        {
            var queryString = "exec CountTotalMeetingsForTeacher @teacher_id;";
            var query = new Query(queryString, connectionString);
            query.AddParameter("@teacher_id", teacherID);
            var results = query.Execute();
            var meetingData = results.Rows.ToDTOList<TeacherTotalMeetings>();
            return meetingData;
        }

        private DataTable ExecuteStoredProcedure(string queryString)
        {
            var query = new Query(queryString, connectionString);
            var results = query.Execute();
            return results;
        }
    }
}