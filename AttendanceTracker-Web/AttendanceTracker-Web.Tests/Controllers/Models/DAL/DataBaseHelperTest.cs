using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DAL;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DTOs.DB;

namespace AttendanceTracker_Web.Tests.Controllers.Models.DAL
{
    [TestClass]
    public class DataBaseHelperTest
    {
        DataBaseHelper dbHelper;
        DataBaseDTOFactory dbDTOFactory;

        [TestInitialize]
        public void Setup()
        {
            dbHelper = new DataBaseHelper();
            dbDTOFactory = new DataBaseDTOFactory();
        }

        [TestMethod]
        public void DoesDeviceExist()
        {
            long imei = 1;
            var result = dbHelper.DoesDeviceExist(imei);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddDevice()
        {
            long deviceID = 5;
            long studentID = 5;
            var device = dbDTOFactory.Device(deviceID, studentID);

            var dto = dbHelper.AddDevice(device);

            Assert.AreEqual(device.DeviceID, dto.DeviceID);
            Assert.AreEqual(device.StudentID, dto.StudentID);
        }

        [TestMethod]
        public void AddStudent()
        {
            long cwid = 8;
            string firstName = "Grace";
            string lastName = "Hopper";
            string email = "ghoper@asdf.com";
            var student = dbDTOFactory.Student(cwid, firstName, lastName, email);

            var dto = dbHelper.AddStudent(student);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
