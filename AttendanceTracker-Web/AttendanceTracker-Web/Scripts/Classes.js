var classesModule = angular.module("ClassesModule", ['ngRoute', 'ngAnimate']);

//classesModule.config(['$locationProvider', function ($locationProvider) {
//    //$locationProvider.hashPrefix('');

//    $locationProvider.html5Mode({
//        enabled: true,
//        requireBase: true
//    });
//}]);

classesModule.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: "/Content/Home/page-classes.html",
            controller: "ClassesPageController"
        })
        .when("/students", {
            templateUrl: "/Content/Home/page-students.html",
            controller: "StudentsPageController"
        });
}]);

var classesController = classesModule.controller("ClassesController", function ($http) {
    var classesController = this;

    classesController.classesResponse = [];
    classesController.classes = [];
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

    classesController.editClassButtonClicked = function () {
        classesController.dialog.showModal();
    };
});

classesController.directive('buildChart', function () {
    return {
        restrict: 'A',
        link: function (element, canvas) {
            angular.element (element).ready(function () {
                var classObj = element.class;
                var chartData = buildAreaLineChartData(classObj.dates, classObj.studentCounts);
                initLineChart(canvas[0], chartData);
            })
        }
    }
});

classesModule.controller("ClassesPageController", function ($scope, $http) {
    $scope.pageClass = "page-classes";
    $scope.classData = [];

    angular.element(document).ready(function () {
        $scope.getClassData();
    });
       
    $scope.getClassData = function () {
        $http.get("/Home/GetClassDataForTeacher").then(
            function (response) {
                $scope.classData = response.data;
            },
            function (error) {
                console.log(error);
            }
        );
    };
});

classesModule.controller("StudentsPageController", function ($scope) {
    $scope.pageClass = "page-students";
});
