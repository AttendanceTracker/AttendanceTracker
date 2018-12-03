angular.module("ClassesModule", [])
    .controller("ClassesController", function ($scope, $http) {
        var classesController = this;

        classesController.classesResponse = [];
        classesController.classes = [];
        classesController.classData = [];
        classesController.dialog = document.querySelector('dialog');
        classesController.showDialogButton = document.querySelector('#show-dialog');

        classesController.$onInit = function () {
            classesController.getClassesData();
        };

        classesController.getClassesData = function () {
            $http.get("/Home/GetAttendanceChartData").then(
                function (response) {
                    classesController.classesResponse = response.data;
                    classesController.buildClasses();
                    classesController.getClassData();
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        classesController.buildClasses = function () {
            classesController.classesResponse.forEach(function (element) {
                var dates = element.map(x => classesController.simpleFormatDate(x.MeetingTime));
                var studentCounts = element.map(x => x.StudentCount);
                var classObj = { name: element[0].ClassName, dates: dates, studentCounts: studentCounts };
                classesController.classes.push(classObj);
            });
        };

        classesController.simpleFormatDate = function (date) {
            var actualDate = new Date(date);
            var dayString = classesController.dateGetDayString(actualDate.getDay());
            var month = actualDate.getMonth() + 1;
            var day = actualDate.getDate();
            var formattedDateString = dayString + " " + month.toString() + "/" + day.toString();
            return formattedDateString;
        };

        classesController.dateGetDayString = function (day) {
            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            return weekday[day];
        };

        classesController.getClassData = function () {
            $http.get("/Home/GetClassDataForTeacher").then(
                function (response) {
                    classesController.classData = response.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        classesController.editClassButtonClicked = function () {
            classesController.dialog.showModal();
        };
    })
    .directive('buildChart', function ($parse) {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs) {
                element.ready(function () {
                    var classObj = JSON.parse(attrs.buildChart);
                    var chartData = buildAreaLineChartData(classObj.dates, classObj.studentCounts);
                    initLineChart(element[0], chartData);
                })
            }
        }
    });
