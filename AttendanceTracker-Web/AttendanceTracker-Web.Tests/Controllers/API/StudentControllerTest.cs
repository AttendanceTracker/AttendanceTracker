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
        WebDTOFactory webDTOFactory;

        [TestInitialize]
        public void Setup()
        {
            studentController = new StudentController(DALDataSource.Test);
            webDTOFactory = new WebDTOFactory();
        }

        [TestMethod]
        public void Register()
        {
            long cwid = 1;
            string firstName = "Jane";
            string lastName = "Doe";
            string email = "jdoe@a.com";
            var student = webDTOFactory.RegisterStudentRequest(cwid, firstName, lastName, email);

            var response = studentController.Register(student) as OkNegotiatedContentResult<RegisterStudentResponse>;

            Assert.AreEqual(cwid, response.Content.CWID);
            Assert.AreEqual(firstName, response.Content.FirstName);
            Assert.AreEqual(lastName, response.Content.LastName);
            Assert.AreEqual(email, response.Content.Email);
        }
    }
}
