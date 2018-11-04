using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DTOs.Web;

namespace AttendanceTracker_Web.Tests.Controllers.Models.Factories
{
    [TestClass]
    public class WebDTOFactoryTest
    {
        WebDTOFactory factory;

        [TestInitialize]
        public void Setup()
        {
            factory = new WebDTOFactory();
        }

        [TestMethod]
        public void RegisterDeviceRequest()
        {
            long imei = 1;
            long studentID = 1;
            var expectedDTO = new RegisterDeviceRequest();
            expectedDTO.IMEI = imei;
            expectedDTO.StudentID = 1;

            var actualDTO = factory.RegisterDeviceRequest(imei, studentID);

            Assert.AreEqual(expectedDTO.IMEI, actualDTO.IMEI);
            Assert.AreEqual(expectedDTO.StudentID, actualDTO.StudentID);
        }

        [TestMethod]
        public void RegisterDeviceResponse()
        {
            long imei = 1;
            long studentID = 1;
            var expectedDTO = new RegisterDeviceResponse();
            expectedDTO.IMEI = imei;
            expectedDTO.StudentID = 1;

            var actualDTO = factory.RegisterDeviceResponse(imei, studentID);

            Assert.AreEqual(expectedDTO.IMEI, actualDTO.IMEI);
            Assert.AreEqual(expectedDTO.StudentID, actualDTO.StudentID);
        }
    }
}
