using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Controllers.API;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;

using System.Web.Http.Results;

namespace AttendanceTracker_Web.Tests.Controllers.API
{
    [TestClass]
    public class DeviceControllerTest
    {
        DeviceController deviceController;
        WebFactory webFactory;

        [TestInitialize]
        public void Setup() {
            DataAccessLayer dal = new DataAccessLayer(DALDataSource.Test);
            deviceController = new DeviceController(dal);
            webFactory = new WebFactory();
        }

        [TestMethod]
        public void Get()
        {
            long imei = 1;
            long studentID = 1;

            var response = deviceController.Get(imei) as OkNegotiatedContentResult<GetDeviceResponse>;

            Assert.AreEqual(imei, response.Content.IMEI);
            Assert.AreEqual(studentID, response.Content.StudentID);
        }

        [TestMethod]
        public void Register()
        {
            long imei = 1;
            long studentID = 1;

            var requestDTO = webFactory.RegisterDeviceRequest(imei, studentID);
            var response = deviceController.Register(requestDTO) as OkNegotiatedContentResult<RegisterDeviceResponse>;

            Assert.AreEqual(imei, response.Content.IMEI);
            Assert.AreEqual(studentID, response.Content.StudentID);
        }

        [TestMethod]
        public void Update()
        {
            long imei = 1;
            long studentID = 1;

            var requestDTO = webFactory.UpdateDeviceRequest(imei, studentID);
            var response = deviceController.Update(requestDTO) as OkNegotiatedContentResult<UpdateDeviceResponse>;

            Assert.AreEqual(imei, response.Content.IMEI);
            Assert.AreEqual(studentID, response.Content.StudentID);
        }
    }
}
