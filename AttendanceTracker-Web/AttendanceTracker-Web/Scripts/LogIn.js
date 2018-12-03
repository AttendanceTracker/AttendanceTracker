angular.module("LoginModule", [])
    .controller("LoginController", function ($scope, $http) {
        loginController = this;

        loginController.logInButtonClicked = function () {
            var requestData = {};
            requestData["Username"] = $("#username-input").val();
            requestData["Password"] = $("#password-input").val();
            var config = { withCredentials: true };
            $http.post("/api/account/SignIn", requestData).then(
                function (response) {
                    var responseString = JSON.stringify(response.data);
                    setCookie(responseString, response.expires);
                    window.location.href = "/Home/Index";
                },
                function (error) {
                    console.log(error);
                }
            );

            //var requestData = {};
            //requestData["Username"] = $("#username-input").val();
            //requestData["Password"] = $("#password-input").val();
            //request("/api/Account/SignIn", "POST", "json", requestData, null, loginSuccess, loginError);
        }

        //loginController.loginSuccess = function (response) {
        //    //var responseString = JSON.stringify(response);
        //    //setCookie(responseString, response.expires);
        //    //window.location.href = "/Home/Index";
        //}

        //loginController.loginError = function (error) {
        //    alert("Could not log in");
        //    console.log(error);
        //}
});
