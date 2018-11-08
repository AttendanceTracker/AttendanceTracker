using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Tests.Models.Factories
{
    [TestClass]
    public class WebFactoryTest
    {
        WebFactory webFactory;

        [TestInitialize]
        public void Setup()
        {
            webFactory = new WebFactory();
        }

        [TestMethod]
        public void GetDeviceResponse()
        {
            var expected = new GetDeviceResponse();
            expected.IMEI = 1;
            expected.StudentID = 1;
            var actualDTO = webFactory.GetDeviceResponse(expected.IMEI, expected.StudentID);
            AssertDevicesAreEqual(expected, actualDTO);
        }

        [TestMethod]
        public void RegisterDeviceRequest()
        {
            var expected = new RegisterDeviceRequest();
            expected.IMEI = 1;
            expected.StudentID = 1;
            var actualDTO = webFactory.RegisterDeviceRequest(expected.IMEI, expected.StudentID);
            AssertDevicesAreEqual(expected, actualDTO);
        }

        [TestMethod]
        public void RegisterDeviceResponse()
        {
            var expected = new RegisterDeviceResponse();
            expected.IMEI = 1;
            expected.StudentID = 1;
            var actualDTO = webFactory.RegisterDeviceResponse(expected.IMEI, expected.StudentID);
            AssertDevicesAreEqual(expected, actualDTO);
        }

        [TestMethod]
        public void UpdateDeviceRequest()
        {
            var expected = new UpdateDeviceRequest();
            expected.IMEI = 1;
            expected.StudentID = 1;
            var actualDTO = webFactory.UpdateDeviceRequest(expected.IMEI, expected.StudentID);
            AssertDevicesAreEqual(expected, actualDTO);
        }

        [TestMethod]
        public void UpdateDeviceResponse()
        {
            var expected = new UpdateDeviceResponse();
            expected.IMEI = 1;
            expected.StudentID = 1;
            var actualDTO = webFactory.UpdateDeviceResponse(expected.IMEI, expected.StudentID);
            AssertDevicesAreEqual(expected, actualDTO);
        }

        void AssertDevicesAreEqual(WebDevice expected, WebDevice actual)
        {

            Assert.AreEqual(expected.IMEI, actual.IMEI);
            Assert.AreEqual(expected.StudentID, actual.StudentID);
        }

        [TestMethod]
        public void RegisterStudentRequest()
        {
            var cwid = 1;
            var firstName = "John";
            var lastName = "Doe";
            var email = "jdoe@a.com";

            var dto = webFactory.RegisterStudentRequest(cwid, firstName, lastName, email);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email); 
        }

        [TestMethod]
        public void RegisterStudentResponse()
        {
            var cwid = 1;
            var firstName = "John";
            var lastName = "Doe";
            var email = "jdoe@a.com";

            var dto = webFactory.RegisterStudentResponse(cwid, firstName, lastName, email);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
