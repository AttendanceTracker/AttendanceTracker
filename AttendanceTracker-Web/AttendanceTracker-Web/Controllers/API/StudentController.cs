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
    public class StudentController : BaseAPIController
    {
        public StudentController() : base()
        {
        }

        public StudentController(DALDataSource dataSource) : base(dataSource)
        {
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterStudentRequest request)
        {
            try
            {
                var student = dbDTOFactory.Student(request.CWID, request.FirstName, request.LastName, request.Email);
                var resultStudent = dal.AddStudent(student);
                var response = webFactory.RegisterStudentResponse(resultStudent.CWID, resultStudent.FirstName, resultStudent.LastName, resultStudent.Email);
                return Ok(response);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
