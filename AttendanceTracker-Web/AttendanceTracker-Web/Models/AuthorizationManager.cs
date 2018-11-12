using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Models
{
    public class AuthorizationManager
    {
        private DataAccessLayer dal;
        private DataBaseFactory dbFactory;

        public AuthorizationManager()
        {
            dal = new DataAccessLayer(DALDataSource.DB);
            dbFactory = new DataBaseFactory();
        }

        public bool IsAuthorized(string token)
        {
            if (token != null)
            {
                var accessToken = dal.Source.GetAccessToken(token);
                var expirationDate = accessToken.Issued.AddSeconds(accessToken.ExpiresIn);
                if(accessToken.DoesExpire && DateTime.Now <= expirationDate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}