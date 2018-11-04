using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.Factories;
using AttendanceTracker_Web.Models.DAL;

namespace AttendanceTracker_Web.Controllers.API
{
    public class BaseAPIController : ApiController
    {
        public DataAccessLayer dal;
        public WebDTOFactory factory;

        public BaseAPIController()
        {
            dal = new DataAccessLayer(DALDataSource.DB);
            factory = new WebDTOFactory();
        }

        public BaseAPIController(DALDataSource dataSource)
        {
            dal = new DataAccessLayer(dataSource);
            factory = new WebDTOFactory();
        }
    }
}
