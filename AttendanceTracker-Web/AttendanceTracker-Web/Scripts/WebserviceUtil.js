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