using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Tests.Models.Factories
{
    [TestClass]
    public class WebDTOFactoryTest
    {
        WebFactory webFactory;

        [TestInitialize]
        public void Setup()
        {
            webFactory = new WebFactory();
        }

        [TestMethod]
        public void RegisterDeviceRequest()
        {
            long imei = 1;
            long studentID = 1;

            var actualDTO = webFactory.RegisterDeviceRequest(imei, studentID);

            Assert.AreEqual(imei, actualDTO.IMEI);
            Assert.AreEqual(studentID, actualDTO.StudentID);
        }

        [TestMethod]
        public void RegisterDeviceResponse()
        {
            long imei = 1;
            long studentID = 1;

            var actualDTO = webFactory.RegisterDeviceResponse(imei, studentID);

            Assert.AreEqual(imei, actualDTO.IMEI);
            Assert.AreEqual(studentID, actualDTO.StudentID);
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
