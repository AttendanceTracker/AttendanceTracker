angular.module("AttendanceModule", [])
    .controller("AttendanceController", function ($scope, $http) {
        var attendance = this;

        attendance.currentDate = new Date();
        attendance.classMeetings = [];
        attendance.meetingDates = [];

        attendance.$onInit = function () {
            attendance.updateAttendance();
            attendance.meetingDates = attendance.classMeetings.flat().map(x => x.MeetingDate);
        };

        attendance.backButtonClicked = function () {
            attendance.addDays(-7);
            attendance.updateAttendance();
        };

        attendance.forwardButtonClicked = function () {
            attendance.addDays(7);
            attendance.updateAttendance();
        };

        attendance.addDays = function (dayCount) {
            attendance.currentDate.setDate(attendance.currentDate.getDate() + dayCount);
        }

        attendance.updateAttendance = function () {
            $http.get("/Home/GetAttendance", { params: { date: attendance.currentDate } }).then(
                function (response) {
                    attendance.classMeetings = response.data;
                    attendance.meetingDates = attendance.classMeetings.flat().map(x => x.MeetingDate);
                },
                function (error) {
                    console.log(error);
                });
        };

        attendance.downloadButtonClicked = function (indexI, indexJ) {
            var meeting = attendance.classMeetings[indexI][indexJ];
            attendance.downloadAttendance(meeting.ID, meeting.MeetingDate);
        }

        attendance.downloadAttendance = function (classID, date) {
            var actualDate = new Date(date);
            var month = actualDate.getMonth() + 1;
            var day = actualDate.getDate();
            var year = actualDate.getFullYear();
            var dateString = month.toString() + "-" + day.toString() + "-" + year.toString();
            var encodedDate = encodeURIComponent(dateString);
            var url = "/Home/GetAttendanceFile?classid=" + classID + "&date=" + encodedDate;
            window.location.href = url;
        };
    })
    .directive('buildChart', function ($parse) {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs) {
                element.ready(function () {
                    //do stuff with attr.meeting
                    var chartData = buildDougnnutChartData(0.86);
                    initDoughnutChart(element[0], chartData);
                })
            }
        }
    });
