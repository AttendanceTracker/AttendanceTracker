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

        [TestMethod]
        public void RegisterStudentRequest()
        {
            var cwid = 1;
            var firstName = "John";
            var lastName = "Doe";
            var email = "jdoe@a.com";

            var dto = factory.RegisterStudentRequest(cwid, firstName, lastName, email);

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

            var dto = factory.RegisterStudentResponse(cwid, firstName, lastName, email);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
