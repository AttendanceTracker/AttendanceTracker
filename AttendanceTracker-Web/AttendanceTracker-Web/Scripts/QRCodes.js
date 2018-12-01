angular.module("QRCodesModule", [])
    .controller("QRCodesController", function ($scope, $http) {
        var qrCodesController = this;

        qrCodesController.qrCodeData = [];
        qrCodesController.classData = [];
        qrCodesController.selectedClass = {};
        qrCodesController.dialog = document.querySelector('dialog');
        qrCodesController.showDialogButton = document.querySelector('#show-dialog');

        qrCodesController.$onInit = function () {
            qrCodesController.getQRCodes();
            qrCodesController.getClasses();
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
        }

        qrCodesController.getClasses = function () {
            $http.get("/Home/GetClasses").then(
                function (response) {
                    qrCodesController.classData = response.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        }

        if (!qrCodesController.dialog.showModal) {
            qrCodesController.dialogPolyfill.registerDialog(qrCodesController.qrCodesController.dialog);
        }

        qrCodesController.addModalButtonClicked = function () {
            var classSelectText = $("#class-select").val();
            if (classSelectText == "") {
                return showToast("Please select a class");
            }
            //var classDataJsonString = "@Json.Encode(Model.ClassData)";
            //var parser = new DOMParser;
            //var dom = parser.parseFromString('<!doctype html><body>' + classDataJsonString, 'text/html');
            //var decodedClassDataJsonString = dom.body.textContent;
            //var classData = JSON.parse(decodedClassDataJsonString);
            //var c = classData.find(function (element) {
            //    return element.Name == classSelectText;
            //});
            var headers = { "AccessToken": getCookie("user").AccessToken };
            request("/Home/AddQRCode?classid=" + c.ID + "&expiresin=10000", "POST", "text", null, headers,
                function () {
                    qrCodesController.dialog.close();
                    qrCodesController.showToast("QR code created");
                    window.location = "/home/qrcodes/"
                },
                function (e) {
                    console.log(e);
                    qrCodesController.showToast("Failed to create QR code");
                }
            );
        }

        qrCodesController.addQRCodeButtonClicked = function () {
            qrCodesController.dialog.showModal();
        }

        qrCodesController.showToast = function (message) {
            var snackbarContainer = document.querySelector('#toast-container');
            'use strict';
            var data = { message: message };
            snackbarContainer.MaterialSnackbar.showSnackbar(data);
        }
    });
