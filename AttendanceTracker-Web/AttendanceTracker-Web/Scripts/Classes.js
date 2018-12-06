var classesModule = angular.module("ClassesModule", ['ngRoute', 'ngAnimate']);

classesModule.run(function ($rootScope, $http) {
    $rootScope.classesResponse = [];
    $rootScope.classes = [];

    $rootScope.getClassesData = function () {
        $http.get("/Home/GetAttendanceChartData").then(
            function (response) {
                $rootScope.classesResponse = response.data;
                $rootScope.buildClasses();
            },
            function (error) {
                console.log(error);
            }
        );
    };

    $rootScope.buildClasses = function () {
        $rootScope.classes = [];
        $rootScope.classesResponse.forEach(function (element) {
            var dates = element.map(x => $rootScope.simpleFormatDate(x.MeetingTime));
            var studentCounts = element.map(x => x.StudentCount);
            var classObj = { name: element[0].ClassName, dates: dates, studentCounts: studentCounts };
            $rootScope.classes.push(classObj);
        });
    };

    $rootScope.simpleFormatDate = function (date) {
        var actualDate = new Date(date);
        var dayString = $rootScope.dateGetDayString(actualDate.getDay());
        var month = actualDate.getMonth() + 1;
        var day = actualDate.getDate();
        var formattedDateString = dayString + " " + month.toString() + "/" + day.toString();
        return formattedDateString;
    };
       
    $rootScope.dateGetDayString = function (day) {
        var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        return weekday[day];
    };
});

classesModule.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: "/Views/Templates/page-classes.html",
            controller: "ClassesPageController"
        })
        .when("/students/:classID/:className", {
            templateUrl: "/Views/Templates/page-students.html",
            controller: "StudentsPageController"
        });
}]);

var classesController = classesModule.controller("ClassesController", function ($scope, $http) {
    var classesController = this;
    classesController.dialog = {};

    classesController.$onInit = function () {
        $scope.getClassesData();
    };

    classesController.editClassButtonClicked = function () {
        classesController.dialog = document.querySelector('#edit-modal');
        if (!classesController.dialog.showModal) {
            dialogPolyfill.registerDialog(classesController.dialog);
        }
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

//------------ classes page controller

classesModule.controller("ClassesPageController", function ($scope, $http) {
    $scope.pageClass = "page-classes";
    $scope.students = [];

    angular.element(document).ready(function () {
        componentHandler.upgradeAllRegistered();
        $scope.getStudentData();
    });
       
    $scope.getStudentData = function () {
        $http.get("/Home/GetClassDataForTeacher").then(
            function (response) {
                $("#edit-modal-back").addClass("hidden");
                $("#edit-modal").attr("style", "width: 410px");
                $scope.students = response.data;
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
                $scope.getClassesData();
                $scope.getStudentData();
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
                $scope.getClassesData();
                $scope.getStudentData();
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

//-------------------------students page controller

classesModule.controller("StudentsPageController", function ($scope, $http, $routeParams) {
    $scope.pageClass = "page-students";
    $scope.class = [];
    $scope.className = $routeParams.className;

    angular.element(document).ready(function () {
        componentHandler.upgradeAllRegistered()
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
                $scope.getClassesData();
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
                $scope.getClassesData();
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
