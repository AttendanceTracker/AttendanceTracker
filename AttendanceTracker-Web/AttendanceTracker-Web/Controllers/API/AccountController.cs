using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web;
using Newtonsoft.Json;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;
using System.Text;

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
                        var accessToken = Login(request.Email, saltedPasswordHash);
                        if (accessToken != null)
                        {
                            var responseContent = webFactory.CreateAccountResponse(teacher.CWID, teacher.FirstName, teacher.LastName, teacher.Email, accessToken.Token);
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
                if (account != null && IsPasswordValid(account.Password, request.Password, account.Salt))
                {
                    var accessToken = Login(account.Username, account.Password);
                    if (accessToken != null)
                    {
                        var teacher = dal.Source.GetTeacherByUserID(account.ID);
                        var responseContent = webFactory.SignInResponse(teacher.CWID, teacher.FirstName, teacher.LastName, teacher.Email, accessToken.Token);
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

        private bool IsPasswordValid(string expectedPassword, string actualPassword, string salt)
        {
            var saltedActualPassword = actualPassword + salt;
            var hashedSaltedActualPassword = saltedActualPassword.SHA256Hash();
            return expectedPassword == hashedSaltedActualPassword;
        }

        private AccessToken Login(string username, string password)
        {
            var account = dal.Source.GetAccount(username);
            var token = Guid.NewGuid().ToString();
            var accessToken = dbFactory.AccessToken(account.ID, token, dayInSeconds, DateTime.Now, true);
            var addedAccessToken = dal.Source.AddAccessToken(accessToken);
            return addedAccessToken;
        }

        private HttpCookie BuildUserCookie(Teacher teacher, AccessToken accessToken)
        {
            var cookieValue = webFactory.SignInResponse(teacher.CWID, teacher.FirstName, teacher.LastName, teacher.Email, accessToken.Token);
            var cookieValueJson = JsonConvert.SerializeObject(cookieValue);
            var cookie = new HttpCookie("user", cookieValueJson);
            cookie.Path = "/";
            cookie.Secure = true;
            if (accessToken.DoesExpire)
            {
                cookie.Expires = accessToken.Issued.AddSeconds(accessToken.ExpiresIn);
            }
            return cookie;
        }

        private string BuildCookieString(HttpCookie cookie)
        {
            var cookieStringBuilder = new StringBuilder(cookie.Name + "=" + cookie.Value);
            if (cookie.Secure)
            {
                cookieStringBuilder.Append("; Secure");
            }
            cookieStringBuilder.Append("; Path=/");
            return cookieStringBuilder.ToString();
        }

        [HttpDelete]
        public IHttpActionResult SignOut()
        {
            try
            {
                var accessToken = Request.Headers.GetValues("AccessToken").FirstOrDefault();
                if (authManager.IsAuthorized(accessToken))
                {
                    dal.Source.RemoveAccessToken(accessToken);
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
