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

        [HttpGet]
        public IHttpActionResult Get(long cwid)
        {
            try
            {
                var student = dal.GetStudent(cwid);
                var response = webFactory.GetStudentResponse(student.CWID, student.FirstName, student.LastName, student.Email);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterStudentRequest request)
        {
            try
            {
                var student = WebStudentToDBStudent(request);
                var resultStudent = dal.AddStudent(student);
                var response = webFactory.RegisterStudentResponse(resultStudent.CWID, resultStudent.FirstName, resultStudent.LastName, resultStudent.Email);
                return Ok(response);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Update([FromBody] UpdateStudentRequest request)
        {
            try
            {
                var student = WebStudentToDBStudent(request);
                var resultStudent = dal.UpdateStudent(student);
                var response = webFactory.UpdateStudentResponse(resultStudent.CWID, resultStudent.FirstName, resultStudent.LastName, resultStudent.Email);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private Student WebStudentToDBStudent(WebStudent webStudent)
        {
            var dbStudent = dbFactory.Student(webStudent.CWID, webStudent.FirstName, webStudent.LastName, webStudent.Email);
            return dbStudent;
        }
    }
}
