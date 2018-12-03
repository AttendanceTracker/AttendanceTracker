using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;
using AttendanceTracker_Web.Models;

namespace AttendanceTracker_Web.Controllers.API
{
    public class BaseAPIController : ApiController
    {
        public DataAccessLayer dal;
        public WebFactory webFactory;
        public DataBaseFactory dbFactory;
        public AuthorizationManager authManager;

        public BaseAPIController()
        {
            dal = new DataAccessLayer(DALDataSource.DB);
            SetupFactories();
        }

        public BaseAPIController(DataAccessLayer dal)
        {
            this.dal = dal;
            SetupFactories();
        }

        private void SetupFactories()
        {
            webFactory = new WebFactory();
            dbFactory = new DataBaseFactory();
            authManager = new AuthorizationManager();
        }

        public NegotiatedContentResult<T> Accepted<T>(T responseContent)
        {
            var response = new NegotiatedContentResult<T>(HttpStatusCode.Accepted, responseContent, this);
            return response;
        }
    }
}
