using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Models
{
    public class AttendanceViewModel
    {
        public IEnumerable<IGrouping<DateTime, TeacherMeetings>> ClassMeetings { get; set; }
    }
}