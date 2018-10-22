using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web;
using AttendanceTracker_Web.Controllers.MVC;

namespace AttendanceTracker_Web.Tests.Controllers.MVC
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Classes()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Classes() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Class()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Class() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Attendance()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Attendance() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void QRCodes()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.QRCodes() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
