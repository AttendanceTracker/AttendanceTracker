using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using AttendanceTracker_Web.Controllers.API;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Tests.Controllers.API
{
    [TestClass]
    public class StudentControllerTest
    {
        StudentController studentController;
        WebFactory webFactory;

        [TestInitialize]
        public void Setup()
        {
            studentController = new StudentController(DALDataSource.Test);
            webFactory = new WebFactory();
        }

        [TestMethod]
        public void Get()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";

            var response = studentController.Get(1) as OkNegotiatedContentResult<GetStudentResponse>;

            Assert.AreEqual(cwid, response.Content.CWID);
            Assert.AreEqual(firstName, response.Content.FirstName);
            Assert.AreEqual(lastName, response.Content.LastName);
            Assert.AreEqual(email, response.Content.Email);
        }

        [TestMethod]
        public void Register()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";
            var student = webFactory.RegisterStudentRequest(cwid, firstName, lastName, email);

            var response = studentController.Register(student) as OkNegotiatedContentResult<RegisterStudentResponse>;

            Assert.AreEqual(cwid, response.Content.CWID);
            Assert.AreEqual(firstName, response.Content.FirstName);
            Assert.AreEqual(lastName, response.Content.LastName);
            Assert.AreEqual(email, response.Content.Email);
        }

        [TestMethod]
        public void Update()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";
            var student = webFactory.UpdateStudentRequest(cwid, firstName, lastName, email);

            var response = studentController.Update(student) as OkNegotiatedContentResult<UpdateStudentResponse>;

            Assert.AreEqual(cwid, response.Content.CWID);
            Assert.AreEqual(firstName, response.Content.FirstName);
            Assert.AreEqual(lastName, response.Content.LastName);
            Assert.AreEqual(email, response.Content.Email);
        }
    }
}
