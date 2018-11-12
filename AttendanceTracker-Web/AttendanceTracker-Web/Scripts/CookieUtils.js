function getCookie(key) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + key + "=");
    if (parts.length === 2) {
        value = parts.pop().split(";").shift();
        var valueJson = JSON.parse(value);
        return valueJson;
    }
}