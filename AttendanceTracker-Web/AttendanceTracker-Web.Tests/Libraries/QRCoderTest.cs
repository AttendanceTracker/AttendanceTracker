using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QRCoder;

namespace AttendanceTracker_Web.Tests.Libraries
{
    [TestClass]
    public class QRCoderTest
    {
        [TestMethod]
        public void GenerateQRCodePlainText()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("C7FB8A0E9809A9D0C5646DCF859E35268B847AAFB2762D6E54D30C4251BB9B4E", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            Assert.IsNotNull(qrCodeImage);
        }

        [TestMethod]
        public void QRCodeAsPNG()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("C7FB8A0E9809A9D0C5646DCF859E35268B847AAFB2762D6E54D30C4251BB9B4E", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeImage.Save(@"Test.png");
            Assert.IsNotNull(qrCodeImage);
        }
    }
}
