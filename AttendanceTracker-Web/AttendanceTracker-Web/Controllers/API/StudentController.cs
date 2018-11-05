using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.DTOs.Web;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DAL;

namespace AttendanceTracker_Web.Controllers.API
{
    public class StudentController : BaseAPIController
    {
        public StudentController() : base()
        {
        }

        public StudentController(DALDataSource dataSource) : base(dataSource)
        {
        }

        [HttpPost]
        public IHttpActionResult Register()
        {
            return Ok();
        }
    }
}
