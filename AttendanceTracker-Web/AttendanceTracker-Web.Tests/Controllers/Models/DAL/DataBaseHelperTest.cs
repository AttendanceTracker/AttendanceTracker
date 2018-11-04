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
            var expectedDTO = dbDTOFactory.Device(deviceID, studentID);

            var dto = dbHelper.AddDevice(deviceID, studentID);

            Assert.AreEqual(expectedDTO.DeviceID, dto.DeviceID);
            Assert.AreEqual(expectedDTO.StudentID, dto.StudentID);
        }
    }
}
