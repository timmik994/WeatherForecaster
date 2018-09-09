//Weather forecast data.
let weatherData;

// Draws chart using Google charts.
function DrawChart() {
    console.log(weatherData);
    let chartData = new google.visualization.DataTable();
    chartData.addColumn('datetime', 'X');
    chartData.addColumn('number', 'minimum temperature');
    chartData.addColumn('number', 'Actual temperature');
    chartData.addColumn('number', 'Maximum temperature');
    for (let record of weatherData) {
        chartData.addRows([
            [new Date(record.forecastTime), record.minTemperature, record.actualTemperature, record.maxTemperature]
        ]);
    }
    var options = {
        title: 'Weather',
        curveType: 'function',
        hAxis: {
            title: 'Date'
        },
        vAxis: {
            title: 'Temperature'
        },
        legend: { position: 'bottom' }
    };
    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
    chart.draw(chartData, options);
}

// loads a data from API.
function LoadData() {
    $.ajax({
        url: "GetForecastAsync",
        success: function (data) {
            weatherData = data;
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(DrawChart);
        }  
    });
}