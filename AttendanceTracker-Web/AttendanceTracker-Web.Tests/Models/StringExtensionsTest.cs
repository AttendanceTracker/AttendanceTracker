using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models;

namespace AttendanceTracker_Web.Tests.Models
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void SHA256()
        {
            var plainText = "hello this is a test";
            var expected = "46a6de6b0b1708aadbd5578b5fd1f309e0181c6eb15c021201d961f6766d9ec2";
            var actual = plainText.SHA256Hash();
            Assert.AreEqual(expected, actual);
        }
    }
}
