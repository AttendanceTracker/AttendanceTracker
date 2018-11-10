using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;
using AttendanceTracker_Web.Controllers.API;
using System.Web.Http.Results;

namespace AttendanceTracker_Web.Tests.Controllers.API
{
    [TestClass]
    public class AttendanceControllerTest
    {
        AttendanceController attendanceController;
        WebFactory webFactory;

        [TestInitialize]
        public void Setup()
        {
            DataAccessLayer dal = new DataAccessLayer(DALDataSource.Test);
            attendanceController = new AttendanceController(dal);
            webFactory = new WebFactory();
        }

        [TestMethod]
        public void CheckIn()
        {
            long classID = 1;
            var requestDTO = webFactory.CheckInRequest(1, "asdf", 87.234m, 23.345m);
            var response = attendanceController.CheckIn(classID, requestDTO) as OkNegotiatedContentResult<bool>;
            Assert.IsTrue(response.Content);
        }
    }
}
