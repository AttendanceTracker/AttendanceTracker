using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.DAL;
using AttendanceTracker_Web.Models.DTOs.Web;

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
            return Ok(0);
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterDeviceRequest request)
        {
            try
            {
                dal.AddDevice(request.IMEI, request.StudentID);
                var response = factory.RegisterDeviceResponse(request.IMEI, request.StudentID);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
