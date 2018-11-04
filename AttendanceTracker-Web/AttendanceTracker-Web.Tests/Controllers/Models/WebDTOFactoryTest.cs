using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DTOs.Web;

namespace AttendanceTracker_Web.Tests.Controllers.Models
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
            string imei = "1";
            long studentID = 1;
            var expectedDTO = new RegisterDeviceRequest();
            expectedDTO.imei = imei;
            expectedDTO.studentID = 1;

            var actualDTO = factory.RegisterDeviceRequest(imei, studentID);

            Assert.AreEqual(expectedDTO.imei, actualDTO.imei);
            Assert.AreEqual(expectedDTO.studentID, actualDTO.studentID);
        }

        [TestMethod]
        public void RegisterDeviceResponse()
        {
            string imei = "1";
            long studentID = 1;
            var expectedDTO = new RegisterDeviceResponse();
            expectedDTO.imei = imei;
            expectedDTO.studentID = 1;

            var actualDTO = factory.RegisterDeviceResponse(imei, studentID);

            Assert.AreEqual(expectedDTO.imei, actualDTO.imei);
            Assert.AreEqual(expectedDTO.studentID, actualDTO.studentID);
        }
    }
}
