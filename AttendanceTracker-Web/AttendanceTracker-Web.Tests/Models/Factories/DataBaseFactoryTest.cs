using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Tests.Models.Factories
{
    [TestClass]
    public class DataBaseFactoryTest
    {
        DataBaseFactory dbFactory;

        [TestInitialize]
        public void Setup()
        {
            dbFactory = new DataBaseFactory();
        }

        [TestMethod]
        public void Device()
        {
            long deviceID = 1;
            long studentID = 1;
            var dto = dbFactory.Device(deviceID, studentID);
            var expectedDTO = new Device();
            expectedDTO.DeviceID = deviceID;
            expectedDTO.StudentID = studentID;
            Assert.AreEqual(expectedDTO.DeviceID, dto.DeviceID);
            Assert.AreEqual(expectedDTO.StudentID, dto.StudentID);
        }

        [TestMethod]
        public void Student()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";

            var dto = dbFactory.Student(cwid, firstName, lastName, email);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
