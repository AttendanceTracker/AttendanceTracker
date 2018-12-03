using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;
using AttendanceTracker_Web.Models;

namespace AttendanceTracker_Web.Controllers.API
{
    public class AttendanceController : BaseAPIController
    {
        public AttendanceController() : base()
        {
        }

        public AttendanceController(DataAccessLayer dal) : base(dal)
        {
        }

        [HttpGet]
        public IHttpActionResult Get(long studentID, long classID)
        {
            try
            {
                var attendance = dal.Source.GetStudentAttendance(studentID, classID);
                return Ok(attendance);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult CheckIn([FromBody] CheckInRequest request)
        {
            try
            {
                var qrCode = dal.Source.GetQRCode(request.Payload);
                if (qrCode != null && IsQRCodeActive(qrCode) && IsStudentInClass(qrCode.ClassID, request.StudentID))
                {
                    var attendance = dbFactory.Attendance(0, qrCode.ClassID, request.StudentID, DateTime.Now, request.Latitude, request.Longitude);
                    dal.Source.AddAttendance(attendance);
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool IsQRCodeActive(QRCode qrCode)
        {
            var expirationDate = qrCode.Issued.AddSeconds(qrCode.ExpiresIn);
            var span = new TimeSpan(0, 1, 0);
            return DateTime.Now.Round(span) < expirationDate.Round(span);
        }

        private bool IsStudentInClass(long classID, long studentID)
        {
            var classData = dal.Source.GetClassDataForStudent(studentID);
            if (classData != null)
            {
                return classData.Any(x => x.ID == classID);
            }
            return false;
        }
    }
}
