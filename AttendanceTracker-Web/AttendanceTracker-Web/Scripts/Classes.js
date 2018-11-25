var currentDate = new Date();

function backButtonClicked() {
    currentDate.setDate(currentDate.getDate() - 7)
}

function forwardButtonClicked() {
    currentDate.setDate(currentDate.getDate() + 7)
}

function updateClasses() {

}
