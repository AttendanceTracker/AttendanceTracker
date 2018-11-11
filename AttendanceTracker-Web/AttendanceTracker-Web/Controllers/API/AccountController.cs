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
    public class AccountController : BaseAPIController
    {
        public AccountController() : base()
        {
        }

        public AccountController(DataAccessLayer dal) : base(dal)
        {
        }

        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody] CreateAccountRequest request)
        {
            try
            {
                var salt = Guid.NewGuid().ToString();
                var saltedPassword = request.Password + salt;
                var saltedPasswordHash = saltedPassword.SHA256Hash();
                var dto = dbFactory.Account(0, request.Email, saltedPasswordHash, salt);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
