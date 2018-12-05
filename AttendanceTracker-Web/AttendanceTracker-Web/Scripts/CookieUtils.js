function setCookie(key, value, expires) {
    var now = new Date();
    now.setTime(now.getTime() + 24 * 3600 * 1000);
    document.cookie = key + "=" + value + "; expires=" + now.toUTCString() + "; path=/;";
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

function deleteCookie(key) {
    document.cookie = key + "=; expires=Thu, 01 Jan 1970 00:00:01 GMT; path=/;";
}
