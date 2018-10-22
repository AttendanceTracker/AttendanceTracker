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
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SignUp()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.SignUp() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
