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
        public WebDTOFactory webDTOFactory;
        public DataBaseDTOFactory dbDTOFactory;

        public BaseAPIController()
        {
            init(DALDataSource.DB);
        }

        public BaseAPIController(DALDataSource dataSource)
        {
            init(dataSource);
        }

        private void init(DALDataSource dataSource)
        {
            dal = new DataAccessLayer(dataSource);
            webDTOFactory = new WebDTOFactory();
            dbDTOFactory = new DataBaseDTOFactory();
        }
    }
}
