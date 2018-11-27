angular.module("AttendanceModule", [])
    .controller("AttendanceController", function ($scope, $http) {
        var attendance = this;

        attendance.currentDate = new Date();
        attendance.classMeetings = [];
        attendance.meetingDates = [];

        attendance.$onInit = function () {
            attendance.updateAttendance();
            attendance.updateMeetingDates();
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
        };

        attendance.updateAttendance = function () {
            var config = { params: { date: attendance.currentDate } };
            $http.get("/Home/GetAttendance", config).then(
                function (response) {
                    attendance.classMeetings = response.data;
                    attendance.updateMeetingDates();
                },
                function (error) {
                    console.log(error);
                });
        };

        attendance.updateMeetingDates = function () {
            attendance.meetingDates = attendance.classMeetings.flat().map(x => attendance.simpleFormatDate(x.MeetingDate));
        };

        attendance.simpleFormatDate = function (date) {
            var actualDate = new Date(date);
            var dayString = attendance.dateGetDayString(actualDate.getDay());
            var month = actualDate.getMonth() + 1;
            var day = actualDate.getDate();
            var formattedDateString = dayString + " " + month.toString() + "/" + day.toString();
            return formattedDateString;
        };

        attendance.dateGetDayString = function (day) {
            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            return weekday[day];
        };

        attendance.getAttendedPercentage = function (classID, date, success, error) {
            var config = { params: { classID: classID, date: date } };
            $http.get("/Home/GetAttendedPercentage", config).then(
                function (response) {
                    success(response.data);
                },
                error);
        };

        attendance.downloadButtonClicked = function (indexI, indexJ) {
            var meeting = attendance.classMeetings[indexI][indexJ];
            attendance.downloadAttendance(meeting.ID, meeting.MeetingDate);
        };

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
                    var attendanceController = $scope.attendanceController;
                    var meeting = JSON.parse(attrs.buildChart);
                    var attendedPercentage = 0;
                    attendanceController.getAttendedPercentage(meeting.ID, meeting.MeetingDate,
                        function (percentage) {
                            attendedPercentage = percentage;
                            var chartData = buildDougnnutChartData(attendedPercentage);
                            initDoughnutChart(element[0], chartData);
                        },
                        function (error) {
                            console.log(error);
                        });
                });
            }
        };
    });
