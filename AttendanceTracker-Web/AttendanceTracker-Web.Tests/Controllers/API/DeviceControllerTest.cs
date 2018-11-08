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
        WebDTOFactory webDTOFactory;

        [TestInitialize]
        public void Setup() {
            deviceController = new DeviceController(DALDataSource.Test);
            webDTOFactory = new WebDTOFactory();
        }

        [TestMethod]
        public void Verify()
        {
            long imei = 1;
            var response = deviceController.Verify(imei) as OkNegotiatedContentResult<long>;
            
            Assert.AreEqual(response.Content, imei);
        }

        [TestMethod]
        public void VerifyNoDevice()
        {
            long imei = 2;
            long expected = 0;
            var response = deviceController.Verify(imei) as OkNegotiatedContentResult<long>;

            Assert.AreEqual(expected, response.Content);
        }

        [TestMethod]
        public void Register()
        {
            long imei = 1;
            long studentID = 1;

            var requestDTO = webDTOFactory.RegisterDeviceRequest(imei, studentID);
            var response = deviceController.Register(requestDTO) as OkNegotiatedContentResult<RegisterDeviceResponse>;

            Assert.AreEqual(response.Content.IMEI, imei);
            Assert.AreEqual(response.Content.StudentID, studentID);
        }
    }
}
