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
        public IHttpActionResult GetForClass(long classID)
        {
            try
            {
                var attendance = dal.Source.GetAttendanceByClassID(classID);
                return Ok(attendance);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult CheckIn(long classID, [FromBody] CheckInRequest request)
        {
            try
            {
                var qrCode = dal.Source.GetQRCode(classID, request.Payload);
                if (IsQRCodeValid(qrCode))
                {
                    var attendance = dbFactory.Attendance(0, classID, request.StudentID, DateTime.Now, request.Latitude, request.Longitude);
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

        private bool IsQRCodeValid(QRCode qrCode)
        {
            if (qrCode != null)
            {
                var expirationDate = qrCode.Issued.AddSeconds(qrCode.ExpiresIn);
                var span = new TimeSpan(0, 1, 0);
                return DateTime.Now.Round(span) < expirationDate.Round(span);
            }
            return false;
        }
    }
}
