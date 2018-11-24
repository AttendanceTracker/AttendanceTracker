using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Models
{
    public class ViewModelsFactory
    {
        public QRCodesViewModel QRCodesViewModel(List<ClassData> classData, List<List<long>> qrCodes)
        {
            var viewModel = new QRCodesViewModel();
            viewModel.ClassData = classData;
            viewModel.QRCodes = qrCodes;
            return viewModel;
        }

        public AttendanceViewModel AttendanceViewModel(IEnumerable<IGrouping<DateTime, TeacherMeetings>> classMeetings)
        {
            var viewModel = new AttendanceViewModel();
            viewModel.ClassMeetings = classMeetings;
            return viewModel;
        }
    }
}