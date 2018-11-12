using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models;
using System.Data;
using System.Collections.Generic;

namespace AttendanceTracker_Web.Tests.Models.DAL
{
    [TestClass]
    public class DataBaseHelperTest
    {
        DataBaseHelper dbHelper;
        DataBaseFactory dbFactory;
        Student genericStudent1;
        Student genericStudent2;
        Student genericStudent3;
        Device genericDevice1;
        Device genericDevice2;
        Attendance genericAttendance1;
        Attendance genericAttendance2;
        QRCode genericQRCode1;
        QRCode genericQRCode2;
        Account genericAccount1;
        Account genericAccount2;
        AccessToken genericAccessToken1;
        AccessToken genericAccessToken2;
        Teacher genericTeacher1;
        Teacher genericTeacher2;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        [TestInitialize]
        public void Setup()
        {
            dbHelper = new DataBaseHelper();
            dbFactory = new DataBaseFactory();

            genericStudent1 = dbFactory.Student(8, "Grace", "Hopper", "email");
            genericStudent2 = dbFactory.Student(4, "Grace", "Hopper", "email");
            genericStudent3 = dbFactory.Student(5, "Grace", "Hopper", "email");
            AddDBTestStudent(genericStudent1);
            AddDBTestStudent(genericStudent3);

            genericDevice1 = dbFactory.Device(5, 5);
            genericDevice2 = dbFactory.Device(8, 8);
            AddDBTestDevice(genericDevice1);

            genericAttendance1 = dbFactory.Attendance(0, 3, 5, DateTime.Now, 33.216111m, -87.538623m);
            genericAttendance2 = dbFactory.Attendance(0, 3, 5, DateTime.Now, 33.216111m, -87.538623m);
            AddDBTestAttendance(ref genericAttendance1);

            genericQRCode1 = dbFactory.QRCode(0, 3, "testasdf", DateTime.Now, 10);
            genericQRCode2 = dbFactory.QRCode(0, 3, "testasdf", DateTime.Now, 10);
            AddDBTestQRCode(ref genericQRCode1);

            genericAccount1 = dbFactory.Account(0, "jdoe@a.com", "a;klsdjf", "a;ldskf");
            genericAccount2 = dbFactory.Account(0, "jdoe@a.com", "jkl;", "a90dfs");
            AddDBTestAccount(ref genericAccount1);

            var token1 = Guid.NewGuid().ToString();
            var token2 = Guid.NewGuid().ToString();
            var now = DateTime.Now;
            var date = new DateTime(now.Year, now.Month, now.Day);
            genericAccessToken1 = dbFactory.AccessToken(genericAccount1.ID, token1, 100000, date, true);
            genericAccessToken2 = dbFactory.AccessToken(genericAccount1.ID, token2, 100000, date, true);
            AddDBTestAccessToken(genericAccessToken1);

            genericTeacher1 = dbFactory.Teacher(3, genericAccount1.ID, "john", "doe", genericAccount1.username);
            genericTeacher2 = dbFactory.Teacher(4, genericAccount1.ID, "john", "doe", genericAccount1.username);
            AddDBTestTeacher(genericTeacher1);
        }

        void AddDBTestStudent(Student student)
        {
            try
            {
                var queryString = string.Format("exec Students_AddStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestDevice(Device device)
        {
            try
            {
                var queryString = string.Format("exec Devices_AddDevice {0}, {1};", device.DeviceID, device.StudentID);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestAttendance(ref Attendance attendance)
        {
            try
            {
                var addQueryString = "exec Attendance_AddAttendance @classID, @studentID, @attendedDate, @latitude, @longitude";
                var addQuery = new Query(addQueryString, connectionString);
                addQuery.AddParameter("@classID", attendance.ClassID);
                addQuery.AddParameter("@studentID", attendance.StudentID);
                addQuery.AddParameter("@attendedDate", attendance.attendedDate);
                addQuery.AddParameter("@latitude", attendance.latitude);
                addQuery.AddParameter("@longitude", attendance.longitude);
                attendance.ID = (long)addQuery.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestQRCode(ref QRCode qrCode)
        {
            try
            {
                var addQueryString = "exec QRCodes_AddQRCode @classID, @payload, @issued, @expires_in;";
                var addQuery = new Query(addQueryString, connectionString);
                addQuery.AddParameter("@classID", qrCode.ClassID);
                addQuery.AddParameter("@payload", qrCode.Payload);
                addQuery.AddParameter("@issued", qrCode.Issued);
                addQuery.AddParameter("@expires_in", qrCode.ExpiresIn);
                qrCode.ID = (long)addQuery.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestAccessToken(AccessToken accessToken)
        {
            try
            {
                var queryString = "exec Access_Tokens_AddAccess_Token @user_id, @token, @expires_in, @issued, @does_expire;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@user_id", accessToken.UserID);
                query.AddParameter("@token", accessToken.Token);
                query.AddParameter("@expires_in", accessToken.ExpiresIn);
                query.AddParameter("@issued", accessToken.Issued);
                query.AddParameter("@does_expire", accessToken.DoesExpire);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestAccount(ref Account account)
        {
            try
            {
                var queryString = "exec Accounts_AddAccount @username, @password, @salt;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@username", account.username);
                query.AddParameter("@password", account.password);
                query.AddParameter("@salt", account.salt);
                account.ID = (long)query.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestTeacher(Teacher teacher)
        {
            try
            {
                var queryString = "exec Teachers_AddTeacher @cwid, @user_id, @first_name, @last_name, @email;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@cwid", teacher.CWID);
                query.AddParameter("@user_id", teacher.UserID);
                query.AddParameter("@first_name", teacher.FirstName);
                query.AddParameter("@last_name", teacher.LastName);
                query.AddParameter("@email", teacher.email);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            RemoveDBTestStudent(genericStudent1.CWID);
            RemoveDBTestStudent(genericStudent2.CWID);
            RemoveDBTestStudent(genericStudent3.CWID);
            RemoveDBTestDevice(genericDevice1.DeviceID);
            RemoveDBTestDevice(genericDevice2.DeviceID);
            RemoveDBTestAttendance(genericAttendance1.ID);
            RemoveDBTestQRCode(genericQRCode1.ID);
            RemoveDBTestAccessToken(genericAccessToken1);
            RemoveDBTestAccessToken(genericAccessToken2);
            RemoveDBTestTeacher(genericTeacher1.CWID);
            RemoveDBTestTeacher(genericTeacher2.CWID);
            RemoveDBTestAccount(genericAccount1.ID);
        }

        void RemoveDBTestStudent(long cwid)
        {
            try
            {
                var queryString = string.Format("exec Students_RemoveStudent {0};", cwid);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestDevice(long imei)
        {
            try
            {
                var queryString = string.Format("exec Devices_RemoveDevice {0}", imei);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestAttendance(long id)
        {
            try
            {
                var queryString = string.Format("exec Attendance_RemoveAttendance {0}", id);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestQRCode(long id)
        {
            try
            {
                var queryString = string.Format("exec QRCodes_RemoveQRCode {0}", id);
                var query = new Query(queryString, connectionString);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestAccessToken(AccessToken accessToken)
        {
            try
            {
                var queryString = "exec Access_Tokens_RemoveAccess_Token @token;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@token", accessToken.Token);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestAccount(long id)
        {
            try
            {
                var queryString = "exec Accounts_RemoveAccount @id;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@id", id);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestTeacher(long cwid)
        {
            try
            {
                var queryString = "exec Teachers_RemoveTeacher @cwid;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@cwid", cwid);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void AddDevice()
        {
            var expected = genericDevice2;
            var actual = dbHelper.AddDevice(expected);
            AssertAreDevicesEqual(expected, actual);
        }


        [TestMethod]
        public void UpdateDevice()
        {
            long studentID = genericStudent1.CWID;
            var expected = dbFactory.Device(genericDevice1.DeviceID, studentID);
            var actual = dbHelper.UpdateDevice(expected);
            AssertAreDevicesEqual(expected, actual);
        }

        [TestMethod]
        public void GetDevice()
        {
            var expected = genericDevice1;
            var actual = dbHelper.GetDevice(expected.DeviceID);
            AssertAreDevicesEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveDevice()
        {
            dbHelper.RemoveDevice(genericDevice1.DeviceID);
            var result = dbHelper.GetDevice(genericDevice1.DeviceID);
            Assert.IsNull(result);
        }

        private void AssertAreDevicesEqual(Device expected, Device actual)
        {
            Assert.AreEqual(expected.DeviceID, actual.DeviceID);
            Assert.AreEqual(expected.StudentID, actual.StudentID);
        }

        [TestMethod]
        public void AddStudent()
        {
            var expected = genericStudent2;
            var actual = dbHelper.AddStudent(expected);
            AssertAreStudentsEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateStudent()
        {
            long cwid = genericStudent1.CWID;
            string firstName = genericStudent1.FirstName;
            string lastName = genericStudent1.LastName;
            string email = "email";
            var expected = dbFactory.Student(cwid, firstName, lastName, email);
            var actual = dbHelper.UpdateStudent(expected);
            AssertAreStudentsEqual(expected, actual);
        }

        [TestMethod]
        public void GetStudent()
        {
            var expected = genericStudent1;
            var actual = dbHelper.GetStudent(genericStudent1.CWID);
            AssertAreStudentsEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveStudent()
        {
            long cwid = genericStudent1.CWID;
            dbHelper.RemoveStudent(cwid);
            var actual = dbHelper.GetStudent(cwid);
            Assert.IsNull(actual);
        }

        private void AssertAreStudentsEqual(Student expected, Student actual)
        {
            Assert.AreEqual(expected.CWID, actual.CWID);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
        }

        [TestMethod]
        public void AddAttendance()
        {
            var expected = genericAttendance2;
            var actual = dbHelper.AddAttendance(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttendanceByID()
        {
            var expected = genericAttendance1;
            var actual = dbHelper.GetAttendance(genericAttendance1.ID);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttendanceByDate()
        {
            var expectedList = new List<Attendance>();
            expectedList.Add(genericAttendance1);
            var actualList = dbHelper.GetAttendance(genericAttendance1.attendedDate);
            AssertAreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetAttendanceByDateRange()
        {
            var expectedList = new List<Attendance>();
            var actualList = dbHelper.GetAttendance(genericAttendance1.attendedDate, genericAttendance1.attendedDate.AddSeconds(1));
            AssertAreEqual(expectedList, actualList);
        }

        private void AssertAreEqual(List<Attendance> expectedList, List<Attendance> actualList)
        {
            if (expectedList.Count == actualList.Count)
            {
                for (int i = 0; i < actualList.Count; i++)
                {
                    var expected = expectedList[i];
                    var actual = actualList[i];
                    AssertAreEqual(expected, actual);
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        private void AssertAreEqual(Attendance expected, Attendance actual)
        {
            var span = new TimeSpan(0, 1, 0);
            var expectedDate = expected.attendedDate.Round(span);
            var actualDate = actual.attendedDate.Round(span);
            Assert.AreEqual(expected.ClassID, actual.ClassID);
            Assert.AreEqual(expected.StudentID, actual.StudentID);
            Assert.AreEqual(expectedDate, actualDate);
            Assert.AreEqual(expected.latitude, actual.latitude);
            Assert.AreEqual(expected.longitude, actual.longitude);
        }

        [TestMethod]
        public void AddQRCode()
        {
            var expected = genericQRCode2;
            var actual = dbHelper.AddQRCode(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetQRCodeByID()
        {
            var expected = genericQRCode1;
            var actual = dbHelper.GetQRCode(genericQRCode1.ID);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetQRCode()
        {
            var expected = genericQRCode1;
            var actual = dbHelper.GetQRCode(expected.ClassID, expected.Payload);
            AssertAreEqual(expected, actual);
        }

        private void AssertAreEqual(QRCode expected, QRCode actual)
        {
            var span = new TimeSpan(0, 1, 0);
            var expectedDate = expected.Issued.Round(span);
            var actualDate = actual.Issued.Round(span);
            Assert.AreEqual(expected.ClassID, actual.ClassID);
            Assert.AreEqual(expected.Payload, actual.Payload);
            Assert.AreEqual(expectedDate, actualDate);
            Assert.AreEqual(expected.ExpiresIn, actual.ExpiresIn);
        }

        [TestMethod]
        public void AddAccessToken()
        {
            var expected = genericAccessToken2;
            var actual = dbHelper.AddAccessToken(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAccessToken()
        {
            var expected = genericAccessToken1;
            var actual = dbHelper.GetAccessToken(expected.Token);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveAccessToken()
        {
            var accessToken = genericAccessToken1;
            dbHelper.RemoveAccessToken(accessToken.Token);
            var result = dbHelper.GetAccessToken(accessToken.Token);
            Assert.IsNull(result);
        }

        private void AssertAreEqual(AccessToken expected, AccessToken actual)
        {
            var span = new TimeSpan(0, 1, 0);
            var expectedDate = expected.Issued.Round(span);
            var actualDate = actual.Issued.Round(span);
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Token, actual.Token);
            Assert.AreEqual(expected.ExpiresIn, actual.ExpiresIn);
            Assert.AreEqual(expectedDate, actualDate);
            Assert.AreEqual(expected.DoesExpire, actual.DoesExpire);
        }

        [TestMethod]
        public void AddAccount()
        {
            var expected = genericAccount2;
            var actual = dbHelper.AddAccount(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateAccount()
        {
            var expected = genericAccount2;
            expected.ID = genericAccount1.ID;
            var actual = dbHelper.UpdateAccount(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAccount()
        {
            var expected = genericAccount2;
            var actual = dbHelper.GetAccount(expected.username);
            AssertAreEqual(expected, actual);
        }

        private void AssertAreEqual(Account expected, Account actual)
        {
            Assert.AreEqual(expected.username, actual.username);
            Assert.AreEqual(expected.password, actual.password);
            Assert.AreEqual(expected.salt, actual.salt);
        }

        [TestMethod]
        public void AddTeacher()
        {
            var expected = genericTeacher2;
            var actual = dbHelper.AddTeacher(expected);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTeacher()
        {
            var expected = genericTeacher1;
            var actual = dbHelper.GetTeacher(expected.CWID);
            AssertAreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTeacherByUserID()
        {
            var expected = genericTeacher1;
            var actual = dbHelper.GetTeacherByUserID(expected.UserID);
            AssertAreEqual(expected, actual);
        }

        private void AssertAreEqual(Teacher expected, Teacher actual)
        {
            Assert.AreEqual(expected.CWID, actual.CWID);
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.email, actual.email);
        }
    }
}
