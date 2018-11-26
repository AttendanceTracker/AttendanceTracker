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
        ViewModelsFactory viewModelsFactory;
        DataAccessLayer dal;

        public HomeController()
        {
            authManager = new AuthorizationManager();
            dbFactory = new DataBaseFactory();
            webFactory = new WebFactory();
            viewModelsFactory = new ViewModelsFactory();
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

        public ActionResult Class()
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
                            var fileFormattedDate = date.ToString("DD.MM.YYYY");
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
                        var classCount = (double)dal.Source.GetClassCount(classID);
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
                        var classData = dal.Source.GetClassDataForTeacher(userCookie.CWID);
                        var qrCodes = new List<List<long>>();
                        foreach (var c in classData)
                        {
                            var codes = dal.Source.GetQRCodes(c.ID);
                            qrCodes.Add(codes);
                        }
                        var viewModel = viewModelsFactory.QRCodesViewModel(classData, qrCodes);
                        return View(viewModel);
                    }
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

        [HttpPost]
        public ActionResult AddQRCode(long classID, int expiresIn)
        {
            try
            {
                var accessToken = Request.Headers.GetValues("AccessToken").FirstOrDefault();
                if (authManager.IsAuthorized(accessToken))
                {
                    var guid = Guid.NewGuid().ToString();
                    var payload = guid + classID.ToString();
                    payload = payload.SHA256Hash();
                    var dbQRCode = dbFactory.QRCode(0, classID, payload, DateTime.Now, expiresIn);
                    var fullPayload = webFactory.QRCodePayload(dbQRCode.ClassID, dbQRCode.Payload);
                    var addedQRCode = dal.Source.AddQRCode(dbQRCode);
                    var qrCodeImage = GenerateQRCode(fullPayload);
                    var qrCodeStream = BitmapToMemoryStream(qrCodeImage);
                    return File(qrCodeStream, "image/bmp");
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null) ;
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

        private Bitmap GenerateQRCode(QRCodePayload payload)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var payloadJson = JsonConvert.SerializeObject(payload);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payloadJson, QRCodeGenerator.ECCLevel.Q);
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