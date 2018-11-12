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

namespace AttendanceTracker_Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        AuthorizationManager authManager;
        DataBaseFactory dbFactory;
        DataAccessLayer dal;

        public HomeController()
        {
            authManager = new AuthorizationManager();
            dbFactory = new DataBaseFactory();
            dal = new DataAccessLayer(DALDataSource.DB);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Classes()
        {
            return View();
        }

        public ActionResult Class()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            return View();
        }

        public ActionResult QRCodes()
        {
            return View();
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
                    var addedQRCode = dal.Source.AddQRCode(dbQRCode);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("C7FB8A0E9809A9D0C5646DCF859E35268B847AAFB2762D6E54D30C4251BB9B4E", QRCodeGenerator.ECCLevel.Q);
                    QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    var a = new MemoryStream();
                    qrCodeImage.Save(a, System.Drawing.Imaging.ImageFormat.Bmp);
                    a.Position = 0;
                    return File(a, "image/bmp");
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}