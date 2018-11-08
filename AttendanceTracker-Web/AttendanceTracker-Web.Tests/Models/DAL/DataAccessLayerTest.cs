using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Tests.Models.DAL
{
    [TestClass]
    public class DataAccessLayerTest
    {
        DataAccessLayer dal;
        DataBaseFactory dbDTOFactory;

        [TestInitialize]
        public void Setup()
        {
            dal = new DataAccessLayer(DALDataSource.Test);
            dbDTOFactory = new DataBaseFactory();
        }

        //[TestMethod]
        //public void DoesDeviceExist()
        //{
        //    long imei = 1;
        //    var doesExist = dal.DoesDeviceExist(imei);
        //    Assert.IsTrue(doesExist);
        //}

        [TestMethod]
        public void AddDevice()
        {
            long imei = 1;
            long studentID = 1;
            var device = dbDTOFactory.Device(imei, studentID);
            var dto = dal.AddDevice(device);
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
            var student = dbDTOFactory.Student(cwid, firstName, lastName, email);

            var dto = dal.AddStudent(student);

            Assert.AreEqual(cwid, dto.CWID);
            Assert.AreEqual(firstName, dto.FirstName);
            Assert.AreEqual(lastName, dto.LastName);
            Assert.AreEqual(email, dto.Email);
        }
    }
}
