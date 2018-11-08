using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Controllers.API
{
    public class DeviceController : BaseAPIController
    {
        public DeviceController(): base()
        {
        }

        public DeviceController(DALDataSource dataSource) : base(dataSource)
        {
        }

        [HttpGet]
        public IHttpActionResult Get(long imei)
        {
            try
            {
                var device = dal.GetDevice(imei);
                var responseDevice = webFactory.GetDeviceResponse(device.DeviceID, device.StudentID);
                return Ok(responseDevice);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterDeviceRequest request)
        {
            try
            {
                var device = WebDeviceToDBDevice(request);
                var resultDevice = dal.AddDevice(device);
                var response = webFactory.RegisterDeviceResponse(resultDevice.DeviceID, resultDevice.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Update([FromBody] UpdateDeviceRequest request)
        {
            try
            {
                var device = WebDeviceToDBDevice(request);
                var resultDevice = dal.UpdateDevice(device);
                var response = webFactory.UpdateDeviceResponse(resultDevice.DeviceID, resultDevice.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    
        private Device WebDeviceToDBDevice(WebDevice webDevice)
        {
            var dbDevice = dbFactory.Device(webDevice.IMEI, webDevice.StudentID);
            return dbDevice;
        }
    }
}
