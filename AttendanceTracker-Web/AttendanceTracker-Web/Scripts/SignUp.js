function signUpButtonClicked() {
    var cwidString = $("#cwid-input").val();
    if (cwidString.length !== 8) {
        alert("Please provide a valid cwid");
        return;
    }
    var cwid = parseInt(cwidString);

    var email = $("#email-input").val();
    if (!email.includes("@") || !email.includes(".")) {
        alert("Please provide a valid email");
        return;
    }

    var password = $("#password-input").val();
    var confirmPassword = $("#confirm-password-input").val();
    if (password !== confirmPassword) {
        alert("Passwords do not match");
        return;
    }

    var requestData = {};
    requestData["CWID"] = cwid;
    requestData["FirstName"] = $("#firstname-input").val();
    requestData["LastName"] = $("#lastname-input").val();
    requestData["Email"] = email;
    requestData["Password"] = password;
    request("/api/account/createaccount", "POST", "json", requestData, null, signUpSuccess, signUpError);
}

function signUpSuccess(response) {
    var responseString = JSON.stringify(response);
    setCookie("user", responseString, response.expires);
    window.location.href = "/Home/Index";
}

function signUpError(error) {
    alert("Could not sign up");
    console.log(error);
}
