$(document).ready(function () {
    var userCookie = getCookie("user");
    $("#username-label").text(userCookie.Username);
});

var signOut = function () {
    var accessToken = getCookie("user").AccessToken;
    var headers = { "AccessToken": accessToken }
        request("/api/Account/SignOut", "DELETE", "json", null, headers, null,
        function () {
            deleteCookie("user");
            window.location.href = "/"
        });
};
