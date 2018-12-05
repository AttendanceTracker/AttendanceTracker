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

    //qrCodesController.addModalButtonClicked = function () {
    //    var classSelectText = $("#class-select").val();
    //    if (classSelectText == "") {
    //        return qrCodesController.showToast("Please select a class.");
    //    }

    //    //var classID = qrCodesController.classData.find(function (element) {
    //    //    return element.Name == classSelectText;
    //    //}).ID;

    //    var config = { params: { classID: $scope.class.ID, expiresIn: 10000 } };
    //    $http.post("/Home/AddQRCode", null, config).then(
    //        function (response) {
    //            if (response.status = 200) {
    //                qrCodesController.getQRCodes();
    //                qrCodesController.dialog.close();
    //                qrCodesController.showToast("QR code created.");
    //            } else {
    //                qrCodesController.showToast("Failed to create QR code.");
    //            }
    //        },
    //        function (error) {
    //            console.log(error);
    //            qrCodesController.showToast("Failed to create QR code.");
    //        }
    //    );
    //};

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
                return
            }

            var config = { params: { classID: $scope.class.ID, expiresIn: 10000 } };
            $http.post("/Home/AddQRCode", null, config).then(
                function (response) {
                    if (response.status = 200) {
                        qrCodesController.getQRCodes();
                        qrCodesController.showToast("QR code created.");
                        $mdDialog.cancel();
                    } else {
                        qrCodesController.showToast("Failed to create QR code.");
                    }
                },
                function (error) {
                    console.log(error);
                    qrCodesController.showToast("Failed to create QR code.");
                }
            );
        };
    }
}]);
