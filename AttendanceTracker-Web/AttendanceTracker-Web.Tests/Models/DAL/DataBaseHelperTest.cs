using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;

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
            AddDBTestStudent(genericStudent1);
            AddDBTestStudent(genericStudent3);
            AddDBTestDevice(genericDevice1);
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

        [TestCleanup]
        public void TearDown()
        {
            RemoveDBTestStudent(genericStudent1.CWID);
            RemoveDBTestStudent(genericStudent2.CWID);
            RemoveDBTestStudent(genericStudent3.CWID);
            RemoveDBTestDevice(genericDevice1.DeviceID);
            RemoveDBTestDevice(genericDevice2.DeviceID);
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
    }
}
