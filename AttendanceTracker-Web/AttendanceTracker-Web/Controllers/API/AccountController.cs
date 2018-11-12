using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;

namespace AttendanceTracker_Web.Controllers.API
{
    public class AccountController : BaseAPIController
    {
        private const int dayInSeconds = 86400;

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
                var account = dbFactory.Account(0, request.Email, saltedPasswordHash, salt);
                var addedAccount = dal.Source.AddAccount(account);
                if (addedAccount != null)
                {
                    var teacher = dbFactory.Teacher(request.CWID, addedAccount.ID, request.FirstName, request.LastName, request.Email);
                    var addedTeacher = dal.Source.AddTeacher(teacher);
                    if (addedTeacher != null)
                    {
                        var accessToken = login(request.Email, saltedPasswordHash);
                        if (accessToken != null)
                        {
                            var responseContent = webFactory.CreateAccountResponse(teacher.CWID, teacher.FirstName, teacher.LastName, teacher.email, accessToken.Token);
                            var response = Accepted(responseContent);
                            return response;
                        }
                    }
                }
                
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult SignIn([FromBody] SignInRequest request)
        {
            try
            {
                var account = dal.Source.GetAccount(request.Username);
                if (isPasswordValid(account.password, request.Password, account.salt))
                {
                    var accessToken = login(account.username, account.password);
                    if (accessToken != null)
                    {
                        var teacher = dal.Source.GetTeacherByUserID(account.ID);
                        var responseContent = webFactory.SignInResponse(teacher.CWID, teacher.FirstName, teacher.LastName, teacher.email, accessToken.Token);
                        return Accepted(responseContent);
                    }
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool isPasswordValid(string expectedPassword, string actualPassword, string salt)
        {
            var saltedActualPassword = actualPassword + salt;
            var hashedSaltedActualPassword = saltedActualPassword.SHA256Hash();
            return expectedPassword == hashedSaltedActualPassword;
        }

        private AccessToken login(string username, string password)
        {
            var account = dal.Source.GetAccount(username);
            var token = Guid.NewGuid().ToString();
            var accessToken = dbFactory.AccessToken(account.ID, token, dayInSeconds, DateTime.Now, true);
            var addedAccessToken = dal.Source.AddAccessToken(accessToken);
            return addedAccessToken;
        }
    }
}
