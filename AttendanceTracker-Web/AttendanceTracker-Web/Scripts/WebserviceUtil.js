var serviceAddress = "https://" + window.location.hostname + "/";
if (window.location.hostname === "localhost") {
    serviceAddress = "https://" + window.location.hostname + ":" + window.location.port + "/";
}
var apiAddress = serviceAddress + "api/";

function request(requestUrl, type, dataType, data, headers, success, error) {
    var xhr = $.ajax({
        url: requestUrl,
        type: type,
        dataType: dataType,
        contentType: "application/json",
        headers: headers,
        data: data === null ? null : type === "get" ? data : JSON.stringify(data),
        success: success,
        error: error
    });

    return xhr;
}