var client = new XMLHttpRequest();
var puzzleInput = "";
var dataPoints = [];
client.open('GET', 'input.txt');
client.onreadystatechange = function() {
  if (client.readyState == 4 && client.status == 200) {
    puzzleInput = client.responseText;
    manipulateData();
  }
}
client.send();

var points = [];
var velocities = [];

function manipulateData() {
  var data = puzzleInput.split('\n');

  data.forEach(elem => {
    elem = elem.replace(/\s/g,"");
    var position = elem.substr(0, elem.indexOf(">")+1).replace("position=", "").replace("<", "").replace(">", "").split(",");
    var point = [parseInt(position[0]), parseInt(position[1])];
    points.push(point);

    var vel = elem.substr(elem.indexOf("velocity")).replace("velocity=", "").replace("<", "").replace(">", "").split(",");
    var velocity = [parseInt(vel[0]), parseInt(vel[1])];
    velocities.push(velocity);
  });

  google.charts.load('current', {'packages': ['corechart']});
  google.charts.setOnLoadCallback(function() {
    drawChart(points);
  });
}

function drawChart(dataPoints) {
  dataPoints.unshift(['X', 'Y']);

  var data = google.visualization.arrayToDataTable(dataPoints);

  var options = {
    /*chartArea: {
      height: '100%',
      width: '100%',
      left: 0,
      top: 0
    },*/
    title: 'Day 10 Chart',
    //hAxis: {title: 'X', minValue: -60000, maxValue: 60000},
    //vAxis: {title: 'Y', minValue: -60000, maxValue: 60000},
    pointSize: 1,
    legend: 'none',
    height: '100%',
    width: '100%'
  };

  var chart = new google.visualization.ScatterChart(document.getElementById('chart_div'));

  chart.draw(data, options);
}

function runSimulation() {
  for (var i = 0; i < 1000; i++) {  
    points.splice(0, 1);

    points.forEach((point, index) => {
      point[0] += velocities[index][0];
      point[1] += velocities[index][1];
    });

    drawChart(points);
    console.log(i);
  }
}