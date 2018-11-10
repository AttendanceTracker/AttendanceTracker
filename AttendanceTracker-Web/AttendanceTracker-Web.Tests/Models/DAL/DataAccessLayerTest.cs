using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Tests.Models.DAL
{
    [TestClass]
    public class DataAccessLayerTest
    {
        DataAccessLayer dal;
        DataBaseFactory dbFactory;

        [TestInitialize]
        public void Setup()
        {
            dal = new DataAccessLayer(DALDataSource.Test);
            dbFactory = new DataBaseFactory();
        }

        [TestMethod]
        public void AddDevice()
        {
            long imei = 1;
            long studentID = 1;
            var device = dbFactory.Device(imei, studentID);
            var dto = dal.Source.AddDevice(device);
            Assert.AreEqual(imei, dto.DeviceID);
            Assert.AreEqual(studentID, dto.StudentID);
        }

        [TestMethod]
        public void AddStudent()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";
            var student = dbFactory.Student(cwid, firstName, lastName, email);

            var dto = dal.Source.AddStudent(student);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
