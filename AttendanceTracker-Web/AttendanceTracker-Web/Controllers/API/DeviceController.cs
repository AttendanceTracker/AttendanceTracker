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

        public DeviceController(DataAccessLayer dal) : base(dal)
        {
        }

        [HttpGet]
        public IHttpActionResult Get(long imei)
        {
            try
            {
                var device = dal.GetDevice(imei);
                if (device != null)
                {
                    var responseDevice = webFactory.GetDeviceResponse(device.DeviceID, device.StudentID);
                    return Ok(responseDevice);
                }
                return Ok();
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
                var device = dbFactory.Device(request.IMEI, request.StudentID);
                var resultDevice = dal.AddDevice(device);
                var response = webFactory.RegisterDeviceResponse(resultDevice.DeviceID, resultDevice.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(long imei, [FromBody] UpdateDeviceRequest request)
        {
            try
            {
                var device = dbFactory.Device(imei, request.StudentID);
                var resultDevice = dal.UpdateDevice(device);
                var response = webFactory.UpdateDeviceResponse(resultDevice.DeviceID, resultDevice.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
