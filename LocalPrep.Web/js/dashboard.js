/* globals Chart:false, feather:false */

(function () {
  'use strict'

  feather.replace({ 'aria-hidden': 'true' })

  // Graphs
  var ctx = document.getElementById('myChart')
  // eslint-disable-next-line no-unused-vars
  var myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: [
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7'
      ],
      datasets: [{
        data: [
          50,
          100,
          1000,
          5000,
          10000,
          7000,
          3000
        ],
        lineTension: 0,
        backgroundColor: '#F7F0E7',
        borderColor: '#CB6319',
        borderWidth: 4,
        pointBackgroundColor: '#CB6319'
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: false
          }
        }]
      },
      legend: {
        display: false
      }
    }
  })
})()
