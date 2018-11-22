using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Controllers.API
{
    public class ClassController : BaseAPIController
    {
        public ClassController() : base()
        {
        }

        public ClassController(DataAccessLayer dal) : base(dal)
        {
        }

        IHttpActionResult GetNames()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
