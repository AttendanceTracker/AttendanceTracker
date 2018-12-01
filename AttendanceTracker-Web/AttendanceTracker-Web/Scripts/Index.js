angular.module("HomeModule", [])
    .controller("HomeController", function ($scope, $http) {
        var homeController = this;

        homeController.classAttendedPercentages = [];

        homeController.$onInit = function () {
            homeController.buildClassAttendedPercentages();
            homeController.getTotalATtendanceDataPoints();
        };

        homeController.buildClassAttendedPercentages = function () {
            homeController.getClassMeetings(function (totalMeetings) {
                homeController.getTotalAttendance(function (totalAttendance) {
                    for (var i = 0; i < totalMeetings.length; i++) {
                        var className = totalAttendance[i].ClassName;
                        var percentage = totalAttendance[i].AttendanceCount / totalMeetings[i].ClassMeetingCount;
                        var classData = { className: className, percentage: percentage };
                        homeController.classAttendedPercentages.push(classData);
                    }
                });
            });
        };

        homeController.getClassMeetings = function (completion) {
            $http.get("/Home/GetTotalMeetings").then(
                function (response) {
                    completion(response.data);
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        homeController.getTotalAttendance = function (completion) {
            $http.get("/Home/GetTotalAttendance").then(
                function (response) {
                    completion(response.data);
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        homeController.getTotalATtendanceDataPoints = function () {
            $http.get("/Home/GetTotalAttendanceChartData").then(
                function (response) {
                    var dates = response.data.map(x => homeController.simpleFormatDate(new Date(x.MeetingTime)));
                    var studentCounts = response.data.map(x => x.StudentCount);
                    var chartData = buildAreaLineChartData(dates, studentCounts);
                    var chart = document.getElementById("attendance-chart");
                    initLineChart(chart, chartData);
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        homeController.simpleFormatDate = function (date) {
            var actualDate = new Date(date);
            var dayString = homeController.dateGetDayString(actualDate.getDay());
            var month = actualDate.getMonth() + 1;
            var day = actualDate.getDate();
            var formattedDateString = dayString + " " + month.toString() + "/" + day.toString();
            return formattedDateString;
        };

        homeController.dateGetDayString = function (day) {
            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            return weekday[day];
        };
    }).directive('buildChart', function ($parse) {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs) {
                element.ready(function () {
                    var attendedPercentage = attrs.buildChart;
                    var chartData = buildDougnnutChartData(attendedPercentage);
                    initDoughnutChart(element[0], chartData);
                });
            }
        };
    });