using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;
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
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        [TestInitialize]
        public void Setup()
        {
            dbHelper = new DataBaseHelper();
            dbFactory = new DataBaseFactory();

            genericStudent1 = dbFactory.Student(8, "Grace", "Hopper", "email");
            genericStudent2 = dbFactory.Student(4, "Grace", "Hopper", "email");
            genericStudent3 = dbFactory.Student(5, "Grace", "Hopper", "email");

            genericDevice1 = dbFactory.Device(5, 5);
            genericDevice2 = dbFactory.Device(8, 8);

            genericAttendance1 = dbFactory.Attendance(0, 3, 5, DateTime.Now, 33.216111m, -87.538623m);
            genericAttendance2 = dbFactory.Attendance(0, 3, 5, DateTime.Now, 33.216111m, -87.538623m);

            genericQRCode1 = dbFactory.QRCode(0, 3, "testasdf", DateTime.Now, 10);
            genericQRCode2 = dbFactory.QRCode(0, 3, "testasdf", DateTime.Now, 10);

            AddDBTestStudent(genericStudent1);
            AddDBTestStudent(genericStudent3);
            AddDBTestDevice(genericDevice1);
            AddDBTestAttendance(ref genericAttendance1);
            AddDBTestQRCode(ref genericQRCode1);
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
            AssertAreAttendancesEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttendanceByID()
        {
            var expected = genericAttendance1;
            var actual = dbHelper.GetAttendance(genericAttendance1.ID);
            AssertAreAttendancesEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttendanceByDate()
        {
            var expectedList = new List<Attendance>();
            expectedList.Add(genericAttendance1);
            var actualList = dbHelper.GetAttendance(genericAttendance1.attendedDate);
            AssertAreAttendanceListsEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetAttendanceByDateRange()
        {
            var expectedList = new List<Attendance>();
            var actualList = dbHelper.GetAttendance(genericAttendance1.attendedDate, genericAttendance1.attendedDate.AddSeconds(1));
            AssertAreAttendanceListsEqual(expectedList, actualList);
        }

        private void AssertAreAttendanceListsEqual(List<Attendance> expectedList, List<Attendance> actualList)
        {
            if (expectedList.Count == actualList.Count)
            {
                for (int i = 0; i < actualList.Count; i++)
                {
                    var expected = expectedList[i];
                    var actual = actualList[i];
                    AssertAreAttendancesEqual(expected, actual);
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        private void AssertAreAttendancesEqual(Attendance expected, Attendance actual)
        {
            var span = new TimeSpan(0, 1, 0);
            var expectedDate = RoundDateTime(expected.attendedDate, span);
            var actualDate = RoundDateTime(actual.attendedDate, span);
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
            AssertAreQRCodesEqual(expected, actual);
        }

        [TestMethod]
        public void GetQRCodeByID()
        {
            var expected = genericQRCode1;
            var actual = dbHelper.GetQRCode(genericQRCode1.ID);
            AssertAreQRCodesEqual(expected, actual);
        }

        [TestMethod]
        public void GetQRCode()
        {
            var expected = genericQRCode1;
            var actual = dbHelper.GetQRCode(expected.ClassID, expected.Payload);
            AssertAreQRCodesEqual(expected, actual);
        }

        private void AssertAreQRCodesEqual(QRCode expected, QRCode actual)
        {
            var span = new TimeSpan(0, 1, 0);
            var expectedDate = RoundDateTime(expected.Issued, span);
            var actualDate = RoundDateTime(actual.Issued, span);
            Assert.AreEqual(expected.ClassID, actual.ClassID);
            Assert.AreEqual(expected.Payload, actual.Payload);
            Assert.AreEqual(expectedDate, actualDate);
            Assert.AreEqual(expected.ExpiresIn, actual.ExpiresIn);
        }

        private DateTime RoundDateTime(DateTime date, TimeSpan span)
        {
            long ticks = (date.Ticks + (span.Ticks / 2) + 1) / span.Ticks;
            return new DateTime(ticks * span.Ticks);
        }
    }
}
