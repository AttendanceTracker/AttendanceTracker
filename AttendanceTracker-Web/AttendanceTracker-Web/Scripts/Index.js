angular.module("HomeModule", [])
    .controller("HomeController", function ($scope, $http) {
        var homeController = this;

        homeController.classAttendedPercentages = [];

        homeController.$onInit = function () {
            homeController.buildClassAttendedPercentages();
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
    }).directive('buildChart', function ($parse) {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs) {
                element.ready(function () {
                    var attendanceController = $scope.attendanceController;
                    var attendedPercentage = attrs.buildChart;
                    var chartData = buildDougnnutChartData(attendedPercentage);
                    initDoughnutChart(element[0], chartData);
                });
            }
        };
    });