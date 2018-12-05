var attenndanceModule = angular.module("AttendanceModule", []);

attenndanceModule.filter('dayOfWeek', function () {
    return function (date) {
        var actualDate = new Date(date);
        var day = actualDate.getDay();
        var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        return weekday[day];
    }
});

attenndanceModule.controller("AttendanceController", function ($scope, $http, $window) {
    var attendance = this;

    attendance.currentDate = new Date();
    attendance.classMeetings = [];

    attendance.$onInit = function () {
        attendance.bindToWindowResize();
        attendance.updateStyle();
        attendance.updateAttendance();
    };

    attendance.bindToWindowResize = function () {
        var w = angular.element($window);
        w.bind('resize', function () {
            var mainContentContainer = document.getElementById("main-content-container");
            var isSmallScreen = angular.element(mainContentContainer).hasClass("is-small-screen")
            attendance.updateStyle(isSmallScreen);
        });
    };

    attendance.updateStyle = function (isSmallScreen) {
        if (!isSmallScreen) {
            $("#back-button").attr("style", "left: 260px");
        } else {
            $("#back-button").attr("style", "left: 20px");
        }
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
            },
            function (error) {
                console.log(error);
            });
    };

    attendance.dateGetDayOfWeekString = function (date) {
        var actualDate = new Date(date);
        var day = actualDate.getDay();
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
});
attenndanceModule.directive('buildChart', function () {
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
