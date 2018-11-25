var currentDate = Date.now

function renderChart(elementID, data) {
    new Chart(document.getElementById(elementID), {
        type: 'doughnut',
        data: data,
        options: {
            tooltips: {
                enabled: false
            }
        }
    });
}

function downloadAttendance(classID, date) {
    var actualDate = new Date(date);
    var month = actualDate.getMonth() + 1;
    var day = actualDate.getDate();
    var year = actualDate.getFullYear();
    var dateString = month.toString() + "-" + day.toString() + "-" + year.toString();
    var encodedDate = encodeURIComponent(dateString);
    var url = "/Home/GetAttendanceFile?classid=" + classID + "&date=" + encodedDate;
    window.location.href = url;
}

function backButtonClicked() {
    currentDate.setDate(currentDate.getDate() - 7)
}

function forwardButtonClicked() {
    currentDate.setDate(currentDate.getDate() + 7)
}

function updateAttendance() {

}
