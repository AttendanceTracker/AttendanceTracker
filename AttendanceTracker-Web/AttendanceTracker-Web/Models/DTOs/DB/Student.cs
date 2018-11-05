using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DTOs.DB
{
    public class Student
    {
        long CWID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
    }
}