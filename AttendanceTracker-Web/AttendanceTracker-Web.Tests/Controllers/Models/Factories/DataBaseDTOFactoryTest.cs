using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DTOs.DB;

namespace AttendanceTracker_Web.Tests.Controllers.Models.Factories
{
    [TestClass]
    public class DataBaseDTOFactoryTest
    {
        DataBaseDTOFactory dbDTOFactory;

        [TestInitialize]
        public void Setup()
        {
            dbDTOFactory = new DataBaseDTOFactory();
        }

        [TestMethod]
        public void Device()
        {
            long deviceID = 1;
            long studentID = 1;
            var dto = dbDTOFactory.Device(deviceID, studentID);
            var expectedDTO = new Device();
            expectedDTO.DeviceID = deviceID;
            expectedDTO.StudentID = studentID;
            Assert.AreEqual(expectedDTO.DeviceID, dto.DeviceID);
            Assert.AreEqual(expectedDTO.StudentID, dto.StudentID);
        }
    }
}
