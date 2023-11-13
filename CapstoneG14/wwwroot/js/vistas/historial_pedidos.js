const Vista_Busqueda = {
  busquedaFecha: () => {
    $("#txtFechaInicio").val("");
    $("#txtFechaFin").val("");
    $("#txtNumeroPedido").val("");

    $(".busqueda-fecha").show();
    $(".busqueda-pedido").hide();
  },
  busquedaVenta: () => {
    $("#txtFechaInicio").val("");
    $("#txtFechaFin").val("");
    $("#txtNumeroPedido").val("");

    $(".busqueda-fecha").hide();
    $(".busqueda-pedido").show();
  },
};
$(document).ready(function () {
  Vista_Busqueda["busquedaFecha"]();
  $.datepicker.setDefaults($.datepicker.regional["es"]);

  $("#txtFechaInicio").datepicker({ dateFormat: "dd/mm/yy" }),
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" });
});

$("#cboBuscarPor").change(function () {
  if ($("#cboBuscarPor").val() == "fecha") Vista_Busqueda["busquedaFecha"]();
  else Vista_Busqueda["busquedaVenta"]();
});

$("#btnBuscar").click(function () {
  if ($("#cboBuscarPor").val() == "fecha") {
    if (
      $("#txtFechaInicio").val().trim() == "" ||
      $("#txtFechaFin").val().trim() == ""
    ) {
      toastr.warning("Debe ingresar un rango de fechas");
      return;
    }
  } else {
    if ($("#txtNumeroPedido").val().trim() == "") {
      toastr.warning("Debe ingresar un numero de pedido");
      return;
    }
  }
  let numeroPedido = $("#txtNumeroPedido").val();
  let fechaInicio = $("#txtFechaInicio").val();
  let fechaFin = $("#txtFechaFin").val();

  $(".card-body").find("div.row").LoadingOverlay("show");
  fetch(
    `/Pedido/Historial?numeroPedido=${numeroPedido}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`
  )
    .then((response) => {
      $(".card-body").find("div.row").LoadingOverlay("hide");
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      $("#tbpedido tbody").html("");
      if (responseJson.length > 0) {
        var labelEstado;
        var labelFecha;
        responseJson.forEach((item) => {
          if (item.estado == 0) {
            labelEstado = '<span class="badge badge-warning">Registrado</span>';
          } else {
            labelEstado = '<span class="badge badge-info">Recibido</span>';
          }
          if (item.fechaEntrega == "" || item.fechaEntrega == null) {
            labelFecha = "en proceso...";
          } else {
            labelFecha = item.fechaEntrega;
          }
          $("#tbpedido tbody").append(
            $("<tr>").append(
              $("<td>").text(item.fechaRegistro),
              $("<td>").text(labelFecha),
              $("<td>").text(item.numeroPedido),
              $("<td>").text(item.tipoDocumentoPedido),
              $("<td>").text(item.tienda),
              $("<td>").html(labelEstado),
              $("<td>").text(item.total),
              $("<td>").append(
                $("<button>")
                  .addClass("btn btn-info btn-sm")
                  .append($("<i>").addClass("fas fa-eye"))
                  .data("pedido", item)
              )
            )
          );
        });
      }
    });
});

$("#tbpedido tbody").on("click", ".btn-info", function () {
  let d = $(this).data("pedido");
  $("#txtFechaRegistro").val(d.fechaRegistro);
  if (d.fechaEntrega != null) $("#txtFechaEntrega").val(d.fechaEntrega);
  else $("#txtFechaEntrega").val("en proceso ...");
  $("#txtNumPedido").val(d.numeroPedido);
  $("#txtUsuarioRegistro").val(d.usuario);
  $("#txtTipoDocumento").val(d.tipoDocumentoPedido);
  $("#txtTienda").val(d.tienda);
  $("#cboEstado").val(d.estado);
  $("#txtTotal").val(d.total);
  $("#tbProductos tbody").html("");
  d.detallePedidos.forEach((item) => {
    $("#tbProductos tbody").append(
      $("<tr>").append(
        $("<td>").text(item.tituloLibro),
        $("<td>").text(item.cantidad),
        $("<td>").text(item.precio),
        $("<td>").text(item.total)
      )
    );
  });
  $("#linkImprimir").attr(
    "href",
    `/Pedido/MostrarPdfPedido?numeroPedido=${d.numeroPedido}`
  );
  $("#modalData").modal("show");
  $("#guardarCambio").click(() => {
    if ($("#cboEstado").val() != 1) {
      console.log("realizando patch");
    }
  });
});
