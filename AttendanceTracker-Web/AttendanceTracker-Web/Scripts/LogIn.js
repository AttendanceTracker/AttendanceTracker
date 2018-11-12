var serviceAddress = "https://" + window.location.hostname + "/";
if (window.location.hostname === "localhost") {
    serviceAddress = "https://" + window.location.hostname + ":" + window.location.port + "/";
}
var apiAddress = serviceAddress + "api/";

function request(requestUrl, type, data, success, error) {
    var xhr = $.ajax({
        url: requestUrl,
        type: type,
        dataType: "json",
        contentType: "application/json",
        data: data === null ? null : type === "get" ? data : JSON.stringify(data),
        success: success,
        error: error
    });

    return xhr;
}

function logInButtonClicked() {
    var requestData = {};
    requestData["Username"] = $("#username-input").val();
    requestData["Password"] = $("#password-input").val();
    request("/api/Account/SignIn", "POST", requestData, loginSuccess, loginError);
}

function loginSuccess(response) {
    var responseString = JSON.stringify(response);
    document.cookie = "user = " + responseString + ";";
    window.location.href = "/Home/Index";
}

function loginError(error) {
    console.log(error);
}

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) return parts.pop().split(";").shift();
}
