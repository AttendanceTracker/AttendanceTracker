using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models.DAL;

namespace AttendanceTracker_Web.Tests.Controllers.Models
{
    [TestClass]
    public class DataBaseHelperTest
    {
        DataBaseHelper dbHelper;

        [TestInitialize]
        public void Setup()
        {
            dbHelper = new DataBaseHelper("Test");
        }

        [TestMethod]
        public void DoesDeviceExist()
        {
            var imei = "1";
            var result = dbHelper.DoesDeviceExist(imei);
            Assert.IsTrue(result);
        }
    }
}
