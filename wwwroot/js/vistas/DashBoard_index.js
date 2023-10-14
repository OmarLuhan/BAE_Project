$(document).ready(function () {
  $("div.container-fluid").LoadingOverlay("show");
  fetch("/DashBoard/obtenerResumen")
    .then((response) => {
      $("div.container-fluid").LoadingOverlay("hide");
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.estado) {
        //mostrar datos en las targetas
        console.log(responseJson.estado);
        let d = responseJson.objeto;
        $("#totalVenta").text(d.totalVentas);
        $("#totalIngresos").text(d.totalIngresos);
        $("#totalLibros").text(d.totalLibros);
        $("#pedidosRestantes").text(d.pedidosRestantes);
        // grafico de barras
        let barchart_labels;
        let barchart_data;

        if (d.ventasUltimaSemana.length > 0) {
          barchart_labels = d.ventasUltimaSemana.map((item) => {
            return item.fecha;
          });
          barchart_data = d.ventasUltimaSemana.map((item) => {
            return item.total;
          });
        } else {
          barchart_labels = [
            "Lunes",
            "Martes",
            "Miércoles",
            "Jueves",
            "Viernes",
            "Sábado",
            "Domingo",
          ];
          barchart_data = [0, 0, 0, 0, 0, 0, 0];
        }
        console.log(barchart_labels);
        //grafico de pie
        let piechart_labels;
        let piechart_data;
        if (d.librosTopUltimaSemana.length > 0) {
          piechart_labels = d.productosTopUltimaSemana.map((item) => {
            return item.producto;
          });
          piechart_data = d.librosTopUltimaSemana.map((item) => {
            return item.cantidad;
          });
        } else {
          piechart_labels = [
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes",
          ];
          piechart_data = [0, 0, 0, 0, 0];
        }

        // Bar Chart Example
        let controlVenta = document.querySelector("#chartVentas");
        console.log(barchart_labels);
        let myBarChart = new Chart(controlVenta, {
          type: "bar",
          data: {
            labels: barchart_labels,
            datasets: [
              {
                label: "Cantidad",
                backgroundColor: "#4e73df",
                hoverBackgroundColor: "#2e59d9",
                borderColor: "#4e73df",
                data: barchart_data,
              },
            ],
          },
          options: {
            maintainAspectRatio: false,
            legend: {
              display: false,
            },
            scales: {
              xAxes: [
                {
                  gridLines: {
                    display: false,
                    drawBorder: false,
                  },
                  maxBarThickness: 50,
                },
              ],
              yAxes: [
                {
                  ticks: {
                    min: 0,
                    maxTicksLimit: 5,
                  },
                },
              ],
            },
          },
        });

        // Pie Chart Example
        let controlLibro = document.querySelector("#chartLibros");
        let myPieChart = new Chart(controlLibro, {
          type: "doughnut",
          data: {
            labels: piechart_labels,
            datasets: [
              {
                data: piechart_data,
                backgroundColor: ["#4e73df", "#1cc88a", "#36b9cc", "#FF785B"],
                hoverBackgroundColor: [
                  "#2e59d9",
                  "#17a673",
                  "#2c9faf",
                  "#FF5733",
                ],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
              },
            ],
          },
          options: {
            maintainAspectRatio: false,
            tooltips: {
              backgroundColor: "rgb(255,255,255)",
              bodyFontColor: "#858796",
              borderColor: "#dddfeb",
              borderWidth: 1,
              xPadding: 15,
              yPadding: 15,
              displayColors: false,
              caretPadding: 10,
            },
            legend: {
              display: true,
            },
            cutoutPercentage: 80,
          },
        });
      }
    });
});
