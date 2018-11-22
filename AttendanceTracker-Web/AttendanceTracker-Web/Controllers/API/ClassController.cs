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

        [HttpGet]
        public IHttpActionResult GetForStudent(long studentID)
        {
            try
            {
                var classData = dal.Source.GetClassDataForStudent(studentID);
                return Ok(classData);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
