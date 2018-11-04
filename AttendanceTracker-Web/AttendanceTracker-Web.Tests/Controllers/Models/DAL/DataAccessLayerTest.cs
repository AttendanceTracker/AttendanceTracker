using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DAL;
using AttendanceTracker_Web.Models.DTOs.DB;

namespace AttendanceTracker_Web.Tests.Controllers.Models.DAL
{
    [TestClass]
    public class DataAccessLayerTest
    {
        DataAccessLayer dal;

        [TestInitialize]
        public void Setup()
        {
            dal = new DataAccessLayer(DALDataSource.Test);
        }

        [TestMethod]
        public void DoesDeviceExist()
        {
            long imei = 1;
            var doesExist = dal.DoesDeviceExist(imei);
            Assert.IsTrue(doesExist);
        }

        [TestMethod]
        public void AddDevice()
        {
            long imei = 1;
            long studentID = 1;
            var dto = dal.AddDevice(imei, studentID);
            Assert.AreEqual(imei, dto.DeviceID);
            Assert.AreEqual(studentID, dto.StudentID);
        }
    }
}
