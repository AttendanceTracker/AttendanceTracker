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

        public StudentController(DataAccessLayer dal) : base(dal)
        {
        }

        [HttpGet]
        public IHttpActionResult Get(long cwid)
        {
            try
            {
                var student = dal.GetStudent(cwid);
                if (student != null)
                {
                    var response = webFactory.GetStudentResponse(student.CWID, student.FirstName, student.LastName, student.Email);
                    return Ok(response);
                }
                return Ok();
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
                var student = dbFactory.Student(request.CWID, request.FirstName, request.LastName, request.Email);
                var resultStudent = dal.AddStudent(student);
                var response = webFactory.RegisterStudentResponse(resultStudent.CWID, resultStudent.FirstName, resultStudent.LastName, resultStudent.Email);
                return Ok(response);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(long cwid, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                var student = dbFactory.Student(cwid, request.FirstName, request.LastName, request.Email);
                var resultStudent = dal.UpdateStudent(student);
                var response = webFactory.UpdateStudentResponse(resultStudent.CWID, resultStudent.FirstName, resultStudent.LastName, resultStudent.Email);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
