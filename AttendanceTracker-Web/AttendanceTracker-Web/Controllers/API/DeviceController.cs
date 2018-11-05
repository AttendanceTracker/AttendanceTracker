using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.DAL;
using AttendanceTracker_Web.Models.DTOs.Web;
using AttendanceTracker_Web.Models.Factories;

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
        public IHttpActionResult Verify(long imei)
        {
            if (dal.DoesDeviceExist(imei))
            {
                return Ok(imei);
            }
            long response = 0;
            return Ok(response);
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterDeviceRequest request)
        {
            try
            {
                var device = dbDTOFactory.Device(request.IMEI, request.StudentID);
                var resultDevice = dal.AddDevice(device);
                var response = webDTOFactory.RegisterDeviceResponse(resultDevice.DeviceID, resultDevice.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
