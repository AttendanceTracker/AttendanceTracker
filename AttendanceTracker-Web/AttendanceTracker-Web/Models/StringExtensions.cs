using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AttendanceTracker_Web.Models
{
    public static class StringExtensions
    {
        public static string SHA256Hash(this string str)
        {
            var stringBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var encoding = Encoding.UTF8;
                var hashedResult = hash.ComputeHash(encoding.GetBytes(str));

                foreach (var b in hashedResult)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}