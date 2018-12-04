using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceTracker_Web.Models.DB;
using AttendanceTracker_Web.Models.Web;
using AttendanceTracker_Web.Models;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace AttendanceTracker_Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        AuthorizationManager authManager;
        DataBaseFactory dbFactory;
        WebFactory webFactory;
        DataAccessLayer dal;

        public HomeController()
        {
            authManager = new AuthorizationManager();
            dbFactory = new DataBaseFactory();
            webFactory = new WebFactory();
            dal = new DataAccessLayer(DALDataSource.DB);
        }

        public ActionResult Index()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        return View();
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetTotalMeetings()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var totalMeetings = dal.Source.GetTeacherTotalMeetings(userCookie.CWID);
                        var totalMeetingsJson = JsonConvert.SerializeObject(totalMeetings);
                        return Content(totalMeetingsJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetTotalAttendance()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var totalAttendance = dal.Source.GetTeacherTotalAttendance(userCookie.CWID);
                        var totalAttendanceJson = JsonConvert.SerializeObject(totalAttendance);
                        return Content(totalAttendanceJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetTotalAttendanceChartData()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var dataPoints = dal.Source.GetTeacherTotalAttendanceData(userCookie.CWID);
                        var dataPointsJson = JsonConvert.SerializeObject(dataPoints);
                        return Content(dataPointsJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        public ActionResult Classes()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        return View();
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetClassDataForTeacher()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var classData = dal.Source.GetClassDataForTeacher(userCookie.CWID);
                        var classDataJson = JsonConvert.SerializeObject(classData);
                        return Content(classDataJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetAttendanceChartData()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var dataPoints = dal.Source.GetTeacherAttendanceData(userCookie.CWID);
                        var groupedDataPoints = dataPoints.GroupBy(x => x.ClassName);
                        var groupedDataPointsJSON = JsonConvert.SerializeObject(groupedDataPoints);
                        return Content(groupedDataPointsJSON);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetClass(long classID)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var students = dal.Source.GetClass(classID);
                        var studentsJson = JsonConvert.SerializeObject(students);
                        return Content(studentsJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpDelete]
        public ActionResult RemoveStudentFromClass(long classID, long studentID)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        dal.Source.RemoveStudentFromClass(classID, studentID);
                        return Content(null);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpPost]
        public ActionResult AddClass(string className)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var classData = dbFactory.ClassData(0, className, userCookie.CWID);
                        dal.Source.AddClassData(classData);
                        return Content(null);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpDelete]
        public ActionResult RemoveClass(long classID)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        dal.Source.RemoveClass(classID);
                        return Content(null);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpPost]
        public ActionResult AddStudentToClass(long classID, long studentID)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        dal.Source.AddStudentToClass(classID, studentID);
                        return Content(null);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        public ActionResult Attendance()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        return View();
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetAttendanceFile(long classID, DateTime date)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var attendanceResults = dal.Source.GetClassAttendance(classID, date);
                        if (attendanceResults.Count > 0)
                        {
                            var stream = new MemoryStream();
                            var writer = new StreamWriter(stream);

                            foreach (var row in attendanceResults)
                            {
                                writer.WriteLine("{0},{1},{2},{3},{4},{5}", row.MeetingDate, row.StudentID, row.FirstName, row.LastName, row.Email, row.DidAttend);
                            }
                            writer.Flush();
                            stream.Position = 0;
                            var fileContents = stream.ToArray();
                            var fileFormattedDate = date.ToString("dd_MM_yyyy");
                            var fileName = string.Format("{0}_{1}.csv", attendanceResults[0].ClassName, fileFormattedDate);
                            return File(fileContents, "application/CSV", fileName);
                        }
                        return null;
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetAttendance(DateTime date)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var startDate = date.WeekStart(DayOfWeek.Sunday);
                        var endDate = startDate.AddDays(6);
                        var classes = dal.Source.GetTeacherMeetings(userCookie.CWID, startDate, endDate);
                        var groupedClasses = classes.GroupBy(x => x.MeetingDate);
                        var groupedClassesJSON = JsonConvert.SerializeObject(groupedClasses);
                        return Content(groupedClassesJSON);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetAttendedPercentage(long classID, DateTime date)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var attendedCount = (double)dal.Source.GetAttendanceCount(classID, date);
                        var classCount = (double)dal.Source.GetStudentCountInClass(classID);
                        var attendedPercentage = attendedCount / classCount;
                        return Content(attendedPercentage.ToString());
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        public ActionResult QRCodes()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        return View();
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetActiveQRCodes()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var activeQRCodes = new List<ActiveQRCode>();
                        var qrCodeDataList = dal.Source.GetActiveQRCodes(userCookie.CWID);
                        foreach (var qrCodeData in qrCodeDataList)
                        {
                            var qrCodePayload = webFactory.QRCodePayload(qrCodeData.ClassID, qrCodeData.Payload);
                            var qrCodeImage = GenerateQRCode(qrCodePayload);
                            byte[] imageData = { };
                            using (var stream = new MemoryStream())
                            {
                                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                imageData = stream.ToArray();
                            }
                            var qrCode = webFactory.ActiveQRCode(qrCodeData.ClassID, qrCodeData.ClassName, qrCodeData.StartDate, qrCodeData.EndDate, imageData);
                            activeQRCodes.Add(qrCode);
                        }
                        var activeQRCodesJson = JsonConvert.SerializeObject(activeQRCodes);
                        return Content(activeQRCodesJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetClasses()
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var classData = dal.Source.GetClassDataForTeacher(userCookie.CWID);
                        var classDataJson = JsonConvert.SerializeObject(classData);
                        return Content(classDataJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpPost]
        public ActionResult AddQRCode(long classID, int expiresIn)
        {
            try
            {
                var userCookieJson = GetCookie("user");
                if (userCookieJson != null)
                {
                    var userCookie = JsonConvert.DeserializeObject<UserCookie>(userCookieJson);
                    if (authManager.IsAuthorized(userCookie.AccessToken))
                    {
                        var guid = Guid.NewGuid().ToString();
                        var payload = guid + classID.ToString();
                        payload = payload.SHA256Hash();
                        var dbQRCode = dbFactory.QRCode(0, classID, payload, DateTime.Now, expiresIn);
                        var fullPayload = webFactory.QRCodePayload(dbQRCode.ClassID, dbQRCode.Payload);
                        var addedQRCode = dal.Source.AddQRCode(dbQRCode);
                        var addedQRCodeJson = JsonConvert.SerializeObject(addedQRCode);
                        return Content(addedQRCodeJson);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        [HttpGet]
        public ActionResult GetQRCode(long qrCodeID)
        {
            try
            {
                var accessToken = Request.Headers.GetValues("AccessToken").FirstOrDefault();
                if (authManager.IsAuthorized(accessToken))
                {
                    Models.DB.QRCode qrCodeData = dal.Source.GetQRCode(qrCodeID);
                    var qrCodePayload = webFactory.QRCodePayload(qrCodeData.ClassID, qrCodeData.Payload);
                    var qrCodeImage = GenerateQRCode(qrCodePayload);
                    var qrCodeStream = BitmapToMemoryStream(qrCodeImage);
                    return File(qrCodeStream, "image/bmp");
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        private string GetCookie(string key)
        {
            if (Request.Cookies[key] != null)
            {
                var value = Request.Cookies[key].Value.ToString();
                return value;
            }
            return null;
        }

        private Bitmap GenerateQRCode(QRCodePayload payload)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var payloadValue = payload.Payload;
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payloadValue, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        private MemoryStream BitmapToMemoryStream(Bitmap qrCodeImage)
        {
            var qrCodeStream = new MemoryStream();
            qrCodeImage.Save(qrCodeStream, System.Drawing.Imaging.ImageFormat.Bmp);
            qrCodeStream.Position = 0;
            return qrCodeStream;
        }

    }
}