var classesModule = angular.module("ClassesModule", ['ngRoute', 'ngAnimate']);

classesModule.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: "/Content/Home/page-classes.html",
            controller: "ClassesPageController"
        })
        .when("/students/:classID/:className", {
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

    classesController.closeModalClicked = function () {
        window.location = "#";
        classesController.dialog.close();
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
                $("#edit-modal-back").addClass("hidden");
                $("#edit-modal").attr("style", "width: 410px");
                $scope.classData = response.data;
            },
            function (error) {
                console.log(error);
            }
        );
    };

    $scope.addClassButtonClicked = function () {
        var addClassText = $("#add-class-textfield").val();
        var config = { params: { className: addClassText} };
        $http.post("/Home/AddClass", null, config).then(
            function (response) {
                $scope.getClassData();
                $scope.showToast("Class added.");
            },
            function (error) {
                $scope.showToast("Failed to add class.");
                console.log(error);
            }
        );
    };

    $scope.removeClassButtonClicked = function (classID) {
        var config = { params: { classID: classID} };
        $http.delete("/Home/RemoveClass", config).then(
            function () {
                $scope.getClassData();
                $scope.showToast("Class removed.");
            },
            function (error) {
                $scope.showToast("Failed to remove class.");
                console.log(error);
            }
        );
    };

    $scope.showToast = function (message) {
        var snackbarContainer = document.querySelector('#toast-container');
        'use strict';
        var data = { message: message };
        snackbarContainer.MaterialSnackbar.showSnackbar(data);
    };
});

classesModule.controller("StudentsPageController", function ($scope, $http, $routeParams) {
    $scope.pageClass = "page-students";
    $scope.class = [];
    $scope.className = $routeParams.className;

    angular.element(document).ready(function () {
        $("#edit-modal-back").removeClass("hidden");
        $("#edit-modal").attr("style", "width: 600px");
        $scope.getClass();
    });

    $scope.getClass = function () {
        var config = { params: { classID: $routeParams.classID } };
        $http.get("/Home/GetClass", config).then(
            function (response) {
                $scope.class = response.data;
            },
            function (error) {
                console.log(error);
            }
        );
    };

    $scope.addStudentButtonClicked = function () {
        var addStudentText = $("#add-student-textfield").val();
        var studentID = parseInt(addStudentText);
        var config = { params: { classID: $routeParams.classID, studentID: studentID } };
        $http.post("/Home/AddStudentToClass", null, config).then(
            function (response) {
                $scope.getClass();
                $scope.showToast("Student added.");
            },
            function (error) {
                $scope.showToast("Failed to add student.");
                console.log(error);
            }
        );
    };

    $scope.removeStudentButtonClicked = function (studentID) {
        var config = { params: { classID: $routeParams.classID, studentID: studentID } };
        $http.delete("/Home/RemoveStudentFromClass", config).then(
            function () {
                $scope.getClass();
                $scope.showToast("Student removed.");
            },
            function (error) {
                $scope.showToast("Failed to remove student.");
                console.log(error);
            }
        );
    };

    $scope.showToast = function (message) {
        var snackbarContainer = document.querySelector('#toast-container');
        'use strict';
        var data = { message: message };
        snackbarContainer.MaterialSnackbar.showSnackbar(data);
    };
});
