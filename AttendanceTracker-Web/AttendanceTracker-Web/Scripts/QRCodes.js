var qrCodesModule = angular.module("QRCodesModule", ["ngAnimate", "ngMaterial", "md.time.picker"]);

qrCodesModule.filter('dayOfWeek', function () {
    return function (date) {
        var actualDate = new Date(date);
        var day = actualDate.getDay();
        var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        return weekday[day];
    }
});

qrCodesModule.controller("QRCodesController", ["$scope", "$http", "$mdDialog", "$mdpTimePicker", function ($scope, $http, $mdDialog) {
    var qrCodesController = this;

    qrCodesController.qrCodeData = [];
    qrCodesController.dialog = {};

    qrCodesController.$onInit = function () {
        qrCodesController.getQRCodes();
    };

    qrCodesController.getQRCodes = function () {
        $http.get("/Home/GetActiveQRCodes").then(
            function (response) {
                qrCodesController.qrCodeData = response.data;
            },
            function (error) {
                console.log(error);
            }
        );
    };

    qrCodesController.addQRCodeButtonClicked = function () {
        qrCodesController.dialog = document.querySelector('#qr-modal');
        angular.element(qrCodesController.dialog).ready(function () {
            componentHandler.upgradeAllRegistered();
        });
        if (!qrCodesController.dialog.showModal) {
            dialogPolyfill.registerDialog(qrCodesController.dialog);
        }
        qrCodesController.dialog.showModal();
    };

    qrCodesController.showToast = function (message) {
        var snackbarContainer = document.querySelector('#toast-container');
        'use strict';
        var data = { message: message };
        snackbarContainer.MaterialSnackbar.showSnackbar(data);
    };

    $scope.showAdvanced = function (ev) {
        $mdDialog.show({
            controller: dialogController,
            templateUrl: '/Views/Templates/QRCodeModal.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
    };

    function dialogController($scope, $http, $mdDialog) {
        $scope.classData = [];
        $scope.class = {};
        $scope.date = new Date();
        $scope.startTime = new Date();
        $scope.endTime = new Date();
        $scope.timePickerError = {
            hour: 'Hour is required',
            minute: 'Minute is required'
        }

        $scope.getClasses = function () {
            $http.get("/Home/GetClasses").then(
                function (response) {
                    $scope.classData = response.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.add = function () {
            if ($scope.class == {}) {
                qrCodesController.showToast("Please select a class.");
                return;
            }

            var expiresIn = $scope.getDurationInSeconds($scope.startTime, $scope.endTime);
            var requestData = { issuedDate: $scope.date, expiresIn: expiresIn };
            var config = { params: { classID: $scope.class.ID }};
            $http.post("/Home/AddQRCode", requestData, config).then(
                function (response) {
                    qrCodesController.getQRCodes();
                    qrCodesController.showToast("QR code created.");
                    $mdDialog.cancel();
                },
                function (error) {
                    console.log(error);
                    qrCodesController.showToast("Failed to create QR code.");
                }
            );
        };

        $scope.getDurationInSeconds = function (startDate, endDate) {
            return Math.round((endDate - startDate) / 1000)
        };
    }
}]);

qrCodesModule.directive("removeTimeModalButton", function () {
    return {
        restrict: "A",
        link: function (scope, element, attr) {
            angular.element(element).ready(function () {
                element.find("button").remove()
            });
        }
    };
});
