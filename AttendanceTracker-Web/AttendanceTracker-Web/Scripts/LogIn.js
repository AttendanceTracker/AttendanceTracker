function logInButtonClicked() {
    var requestData = {};
    requestData["Username"] = $("#username-input").val();
    requestData["Password"] = $("#password-input").val();
    request("/api/Account/SignIn", "POST", "json", requestData, null, loginSuccess, loginError);
}

function loginSuccess(response) {
    var responseString = JSON.stringify(response);
    setCookie(responseString, response.expires);
    window.location.href = "/Home/Index";
}

function loginError(error) {
    alert("Could not log in");
    console.log(error);
}
