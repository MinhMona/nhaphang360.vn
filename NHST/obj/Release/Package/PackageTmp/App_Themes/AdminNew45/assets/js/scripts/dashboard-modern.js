// Dashboard - Modern
//----------------------

(function(window, document, $) {
  //format Money
  function formatMoney(
    amount,
    decimalCount = 2,
    decimal = ".",
    thousands = ","
  ) {
    try {
      decimalCount = Math.abs(decimalCount);
      decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

      const negativeSign = amount < 0 ? "-" : "";

      let i = parseInt(
        (amount = Math.abs(Number(amount) || 0).toFixed(decimalCount))
      ).toString();
      let j = i.length > 3 ? i.length % 3 : 0;

      return (
        negativeSign +
        (j ? i.substr(0, j) + thousands : "") +
        i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands)
      );
    } catch (e) {
      console.log(e);
    }
  }

  // Tỉ lệ đơn hàng mua hộ
  // -----------
  var sum = function(a, b) {
    return a + b;
  };
  var options = {
    labelInterpolationFnc: function(value, idx) {
      return value[0];
    },
    labelPosition: "outside",
    plugins: [
      Chartist.plugins.tooltip({
        tooltipFnc: function(meta, value) {
          return value + " Đơn hàng";
        },
        appendToBody: true,
        class: "pie-tooltip"
      })
    ],
    labelOffset: 10,
    labelDirection: "explode",
    labelInterpolationFnc: function(value, idx) {
      var datas = MuaHangHoChartPie.data.series;
      function getSum(total, num) {
        return total + num;
      }
      var percentage =
        Math.round((datas[idx] / datas.reduce(getSum)) * 100) + "%";
      return value + " / " + percentage;
    },
    chartPadding: 35
  };

  var responsiveOptions = [
    [
      "screen and (min-width: 1200px)",
      {
        labelOffset: 10,
        labelDirection: "explode",
        labelInterpolationFnc: function(value, idx) {
          var datas = MuaHangHoChartPie.data.series;
          function getSum(total, num) {
            return total + num;
          }
          var percentage =
            Math.round((datas[idx] / datas.reduce(getSum)) * 100) + "%";
          return value + " / " + percentage;
        },
        chartPadding: 35
      }
    ],
    [
      "screen and (max-width: 480px)",
      {
        labelOffset: 10,
        labelDirection: "explode",
        labelInterpolationFnc: function(value, idx) {
          var datas = MuaHangHoChartPie.data.series;
          function getSum(total, num) {
            return total + num;
          }
          var percentage =
            Math.round((datas[idx] / datas.reduce(getSum)) * 100) + "%";
          var valueSub = value.substring(0, value.indexOf(" "));
          console.log(valueSub);
          return value[0] + value[1] + value[2] + " / " + percentage;
        },
        chartPadding: 60
      }
    ]
  ];
  var MuaHangHoChartPie = new Chartist.Pie(
    "#muahangho-ratio-pie-chart",
    {
      labels: [
        "Trạng thái 1",
        "Trạng thái 2",
        "Trạng thái 3",
        "Trạng thái 4",
        "Trạng thái 5",
        "Trạng thái 6"
      ],
      series: [50, 25, 55, 22, 50, 18]
    },
    options,
    responsiveOptions
  );

  // Tổng đơn hàng trong tuần
  var TotalOrderWeekBarChart = new Chartist.Bar(
    "#user-statistics-bar-chart",
    {
      labels: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
      ],
      series: [
        [33, 30, 12, 14, 22, 27, 33],
        [11, 29, 55, 47, 40, 33, 24],
        [22, 39, 45, 27, 50, 53, 34]
      ]
    },
    {
      // Default mobile configuration
      stackBars: true,
      chartPadding: 0,
      axisX: {
        showGrid: false
      },
      axisY: {
        showGrid: true,
        labelInterpolationFnc: function(value) {
          return value;
        },
        scaleMinSpace: 20
      },
      plugins: [
        Chartist.plugins.tooltip({
          class: "user-statistics-tooltip",
          appendToBody: true
        })
      ]
    },
    [
      // Options override for media > 800px
      [
        "screen and (min-width: 800px)",
        {
          stackBars: false,
          seriesBarDistance: 10
        }
      ],
      // Options override for media > 1000px
      [
        "screen and (min-width: 1000px)",
        {
          reverseData: false,
          horizontalBars: false,
          seriesBarDistance: 15
        }
      ]
    ]
  );

  TotalOrderWeekBarChart.on("draw", function(data) {
    var dataMHH = TotalOrderWeekBarChart.data.series[0];
    var dataVCH = TotalOrderWeekBarChart.data.series[1];
    var dataTTH = TotalOrderWeekBarChart.data.series[2];
    var totalMHH = dataMHH.reduce((average, data, index, dataMHH) => {
      return (average += data);
    }, 0);
    var totalVCH = dataVCH.reduce((average, data, index, dataVCH) => {
      return (average += data);
    }, 0);
    var totalTTH = dataTTH.reduce((average, data, index, dataTTH) => {
      return (average += data);
    }, 0);
    $(".total-muahangho").text(Math.floor(totalMHH));
    $(".total-vanchuyenho").text(Math.floor(totalVCH));
    $(".total-thanhtoanho").text(Math.floor(totalTTH));
    if (data.type === "bar") {
      data.element.attr({
        style: "stroke-width: 16px",
        x1: data.x1 + 0.001
      });
      data.group.append(
        new Chartist.Svg(
          "circle",
          {
            cx: data.x2,
            cy: data.y2,
            r: 8
          },
          "ct-slice-pie"
        )
      );
      data.element.animate({
        y2: {
          begin: 500,
          dur: 500,
          from: data.y1,
          to: data.y2
        }
      });
    }
  });

  TotalOrderWeekBarChart.on("created", function(data) {
    var defs = data.svg.querySelector("defs") || data.svg.elem("defs");
    defs
      .elem("linearGradient", {
        id: "barGradient1",
        x1: 0,
        y1: 0,
        x2: 0,
        y2: 1
      })
      .elem("stop", {
        offset: 0,
        "stop-color": "rgba(255,75,172,1)"
      })
      .parent()
      .elem("stop", {
        offset: 1,
        "stop-color": "rgba(255,75,172, 0.6)"
      });

    defs
      .elem("linearGradient", {
        id: "barGradient2",
        x1: 0,
        y1: 0,
        x2: 0,
        y2: 1
      })
      .elem("stop", {
        offset: 0,
        "stop-color": "rgba(129,51,255,1)"
      })
      .parent()
      .elem("stop", {
        offset: 1,
        "stop-color": "rgba(129,51,255, 0.6)"
      });
    return defs;
  });

  //Tổng tiền khách nạp  trong tuần

  var TotalGuestDonate = new Chartist.Bar(
    "#total-guest-donate-bar-chart",
    {
      labels: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
      ],
      series: [[4200000, 3000000, 1200000, 1400000, 2200000, 2700000, 500000]]
    },
    {
      // Default mobile configuration
      stackBars: true,
      chartPadding: 15,
      axisX: {
        showGrid: false
      },
      axisY: {
        showGrid: true,
        labelInterpolationFnc: function(value) {
          return value;
        },
        scaleMinSpace: 10,
        onlyInteger: true
      },
      plugins: [
        Chartist.plugins.tooltip({
          class: "user-statistics-tooltip",
          appendToBody: true
        })
      ]
    },
    [
      // Options override for media > 800px
      [
        "screen and (min-width: 800px)",
        {
          stackBars: false,
          seriesBarDistance: 10
        }
      ],
      // Options override for media > 1000px
      [
        "screen and (min-width: 1000px)",
        {
          reverseData: false,
          horizontalBars: false,
          seriesBarDistance: 15
        }
      ]
    ]
  );

  TotalGuestDonate.on("draw", function(data) {
    var dataDonate = TotalGuestDonate.data.series[0];
    var totalDonate = dataDonate.reduce((average, data, index, dataDonate) => {
      return (average += data);
    }, 0);
    $(".total-guest-donate").text(formatMoney(Math.floor(totalDonate)) + " đ");
    if (data.type === "bar") {
      data.element.attr({
        style: "stroke-width: 2rem",
        x1: data.x1 + 0.001
      });
      data.element.animate({
        y2: {
          begin: 500,
          dur: 500,
          from: data.y1,
          to: data.y2
        }
      });
    }
  });

  TotalGuestDonate.on("created", function(data) {
    var defs = data.svg.querySelector("defs") || data.svg.elem("defs");
    defs
      .elem("linearGradient", {
        id: "barGradient1",
        x1: 0,
        y1: 0,
        x2: 0,
        y2: 1
      })
      .elem("stop", {
        offset: 0,
        "stop-color": "rgba(255,75,172,1)"
      })
      .parent()
      .elem("stop", {
        offset: 1,
        "stop-color": "rgba(255,75,172, 0.6)"
      });

    defs
      .elem("linearGradient", {
        id: "barGradient2",
        x1: 0,
        y1: 0,
        x2: 0,
        y2: 1
      })
      .elem("stop", {
        offset: 0,
        "stop-color": "rgba(129,51,255,1)"
      })
      .parent()
      .elem("stop", {
        offset: 1,
        "stop-color": "rgba(129,51,255, 0.6)"
      });
    return defs;
  });
})(window, document, jQuery);
