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
