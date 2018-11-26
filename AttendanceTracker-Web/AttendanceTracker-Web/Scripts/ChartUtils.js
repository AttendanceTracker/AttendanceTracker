function sizeChart(chart, container) {
    var context = chart.getContext("2d");
    context.width = container.innerWidth;
    context.height = container.innerHeight;
}

function buildDougnnutChartData(percentage) {
    var filled = percentage * 100;
    var unfilled = filled - 100;

    Chart.pluginService.register({
        beforeDraw: function (chart) {
            var width = chart.chart.width,
                height = chart.chart.height,
                ctx = chart.chart.ctx;

            chart.clear();

            ctx.restore();
            var fontSize = (height / 114).toFixed(2);
            ctx.font = fontSize + "em sans-serif";
            ctx.textBaseline = "middle";

            var text = filled.toString() + "%",
                textX = Math.round((width - ctx.measureText(text).width) / 2),
                textY = height / 2;

            ctx.fillStyle = "#757575";
            ctx.fillText(text, textX, textY);
            ctx.save();
        }
    });
    
    data = {
        datasets: [
            {
                backgroundColor: ["#536dfe", "#c2cbff"],
                data: [filled, unfilled]
            }
        ]
    };
    return data;
}

function buildAreaLineChartData(labels, dataPoints) {
    data = {
        labels: labels,
        datasets: [
            {
                backgroundColor: "#536dfe",
                lineTension: 0,
                fill: "start",
                pointHoverBackgroundColor: "#009688",
                pointBackgroundColor: "#4ebaaa",
                pointRadius: 4,
                pointHoverRadius: 5,
                data: dataPoints
            }
        ]
    }
    return data;
}

function initDoughnutChart(canvas, data) {
    var context = canvas.getContext("2d");
    //context.fillText("Hello World", 30, 30);
    new Chart(context, {
        type: 'doughnut',
        data: data,
        options: {
            tooltips: {
                enabled: false
            }
        }
    });
}

function initLineChart(canvas, data) {
    var context = canvas.getContext("2d");
    new Chart(context, {
        type: "line",
        data: data,
        options: {
            bezierCurve: false,
            legend: {
                display: false
            }
        }
    });
}
