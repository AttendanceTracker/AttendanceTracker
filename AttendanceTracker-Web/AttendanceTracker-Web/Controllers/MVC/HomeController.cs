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

                    var qrCodeImage = GenerateQRCode(payload);
                    var qrCodeStream = bitmapToMemoryStream(qrCodeImage);
                    return File(qrCodeStream, "image/bmp");
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null) ;
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
                    var qrCodeData = dal.Source.GetQRCode(qrCodeID);
                    var qrCodeImage = GenerateQRCode(qrCodeData.Payload);
                    var qrCodeStream = bitmapToMemoryStream(qrCodeImage);
                    return File(qrCodeStream, "image/bmp");
                }
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, null);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, null);
            }
        }

        private Bitmap GenerateQRCode(string payload)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        private MemoryStream bitmapToMemoryStream(Bitmap qrCodeImage)
        {
            var qrCodeStream = new MemoryStream();
            qrCodeImage.Save(qrCodeStream, System.Drawing.Imaging.ImageFormat.Bmp);
            qrCodeStream.Position = 0;
            return qrCodeStream;
        }

    }
}