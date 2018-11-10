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
    public class AttendanceController : BaseAPIController
    {
        public AttendanceController() : base()
        {
        }

        public AttendanceController(DataAccessLayer dal) : base(dal)
        {
        }
    }
}
