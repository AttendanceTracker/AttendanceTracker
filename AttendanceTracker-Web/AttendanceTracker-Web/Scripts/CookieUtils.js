function setCookie(value, expires) {
    var now = new Date();
    now.setTime(now.getTime() + 24 * 3600 * 1000);
    document.cookie = "user=" + value + "; expires=" + now.toUTCString() + ";";
}

function getCookie(key) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + key + "=");
    if (parts.length === 2) {
        value = parts.pop().split(";").shift();
        var valueJson = JSON.parse(value);
        return valueJson;
    }
}
