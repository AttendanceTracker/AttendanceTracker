using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Controllers.API;
using AttendanceTracker_Web.Models.DAL;

namespace AttendanceTracker_Web.Tests.Controllers.API
{
    [TestClass]
    public class StudentControllerTest
    {
        StudentController studentController;

        [TestInitialize]
        public void Setup()
        {
            studentController = new StudentController(DALDataSource.Test);
        }

        [TestMethod]
        public void Register()
        {
            
        }
    }
}
