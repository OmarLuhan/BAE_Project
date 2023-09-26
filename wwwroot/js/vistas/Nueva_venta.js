let valorImpuesto = 0;
$(document).ready(function () {
  fetch("/Venta/ListaTipoDocumentoVenta")
    .then((response) => {
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.length > 0) {
        responseJson.forEach((item) => {
          $("#cboTipoDocumentoVenta").append(
            $("<option>").val(item.idTipoDocumentoVenta).text(item.descripcion)
          );
        });
      }
    });

  fetch("/Negocio/Obtener")
    .then((response) => {
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.estado) {
        const d = responseJson.objeto;
        $("#inputGroupSubTotal").text(`Subtotal-${d.simboloMoneda}`);
        $("#inputGroupIGV").text(
          `IGV-(${d.porcentajeImpuesto}%)-${d.simboloMoneda}`
        );
        $("#inputGroupTotal").text(`Total-${d.simboloMoneda}`);
        valorImpuesto = parseFloat(d.porcentajeImpuesto);
      }
    });

  $("#cboBuscarProducto").select2({
    ajax: {
      url: "/Venta/ObtenerProductos",
      dataType: "json",
      constentType: "application/json; charset=utf-8",
      delay: 250,
      data: function (params) {
        return {
          busqueda: params.term,
        };
      },
      processResults: function (data) {
        return {
          results: data.map((item) => ({
            id: item.idProducto,
            text: item.descripcion,

            marca: item.marca,
            categoria: item.nombreCategoria,
            urlImagen: item.urlImagen,
            precio: parseFloat(item.precio),
          })),
        };
      },
    },
    language: "es",
    placeholder: "Buscar Producto...",
    minimumInputLength: 1,
    templateResult: formatRepo,
  });
});

function formatRepo(data) {
  if (data.loading) return data.text;

  var contenedor = $(
    `
        <table width="100%">
         <tr>
             <td style="width:60px">
                 <img style="height:60px;width:60px;margin-right:10px" src="${data.urlImagen}"/>
            </td>
            <td>
                 <p style="font-weight:bolder;margin:2px"> ${data.marca}</p>
                 <p style="margin:2px">${data.text}</p>
            </td>
        </tr>
        </table>
        `
  );
  return contenedor;
}
$(document).on("select2:open", () => {
  document.querySelector(".select2-search__field").focus();
});
let productosParaVenta = [];
$("#cboBuscarProducto").on("select2:select", function (e) {
  const data = e.params.data;
  console.log(data);
  let productoEncontrado = productosParaVenta.filter(
    (p) => p.idProducto == data.id
  );
  if (productoEncontrado.length > 0) {
    $("#cboBuscarProducto").val("").trigger("change");
    toastr.warning("", "El producto ya fue agregado");
    return false;
  }

  swal(
    {
      title: data.marca,
      text: data.text,
      imageUrl: data.urlImagen,
      type: "input",
      showCancelButton: true,
      closeOnConfirm: false,
      inputPlaceholder: "Ingrese Cantidad",
    },
    function (valor) {
      if (valor === false) return false;
      if (valor === "") {
        toastr.warning("", "Nesecita ingesar la cantidad");
        return false;
      }
      if (isNaN(parseInt(valor))) {
        toastr.warning("", "debe ingresar un valor numerico");
        return false;
      }
      let producto = {
        idProducto: data.id,
        marcaProducto: data.marca,
        descripcionProducto: data.text,
        categoriaProducto: data.categoria,
        cantidad: parseInt(valor),
        precio: data.precio.toString(),
        total: (parseFloat(valor) * data.precio).toString(),
      };
      productosParaVenta.push(producto);
      mostrarProducto_precios();
      $("#cboBuscarProducto").val("").trigger("change");
      swal.close();
    }
  );
});

function mostrarProducto_precios() {
  let total = 0,
    igv = 0,
    subtotal = 0;
  let porcentaje = valorImpuesto / 100;

  $("#tbProducto tbody").html("");
  productosParaVenta.forEach((item) => {
    total += parseFloat(item.total);
    $("#tbProducto tbody").append(
      $("<tr>").append(
        $("<td>").append(
          $("<button>")
            .addClass("btn btn-danger btn-eliminar btn-sm")
            .append($("<i>").addClass("fa fa-trash-alt"))
            .data("idProducto", item.idProducto)
        ),
        $("<td>").text(item.descripcionProducto),
        $("<td>").text(item.cantidad),
        $("<td>").text(item.precio),
        $("<td>").text(item.total)
      )
    );
  });

  subtotal = total / (1 + porcentaje);
  igv = total - subtotal;

  $("#txtSubTotal").val(subtotal.toFixed(2));
  $("#txtIGV").val(igv.toFixed(2));
  $("#txtTotal").val(total.toFixed(2));
}
$(document).on("click", "button.btn-eliminar", function () {
  const _idproducto = $(this).data("idProducto");
  productosParaVenta = productosParaVenta.filter(
    (p) => p.idProducto != _idproducto
  );
  mostrarProducto_precios();
});
$("#btnTerminarVenta").click(function () {
  if (productosParaVenta.length < 1) {
    toastr.warning("", "debe ingresar productos");
    return;
  }
  const vmDetalleVenta = productosParaVenta;
  const venta = {
    idTipoDocumentoVenta: $("#cboTipoDocumentoVenta").val(),
    documentoCliente: $("#txtDocumentoCliente").val(),
    nombreCliente: $("#txtNombreCliente").val(),
    subTotal: $("#txtSubTotal").val(),
    impuestoTotal: $("#txtIGV").val(),
    total: $("#txtTotal").val(),
    DetalleVenta: vmDetalleVenta,
  };
  $("#btnTerminarVenta").LoadingOverlay("show");
  fetch("/Venta/RegistrarVenta", {
    method: "POST",
    headers: { "Content-Type": "application/json;charset=utf-8" },
    body: JSON.stringify(venta),
  })
    .then((response) => {
      $("#btnTerminarVenta").LoadingOverlay("hide");
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.estado) {
        productosParaVenta = [];
        mostrarProducto_precios();
        $("#txtDocumentoCliente").val("");
        $("#txtNombreCliente").val("");
        $("#cboTipoDocumentoVenta").val(
          $("#cboTipoDocumentoVenta option:first").val()
        );
        swal(
          "Registrado!",
          `Numero venta: ${responseJson.objeto.numeroVenta}`,
          "success"
        );
      } else {
        swal("Lo sentimos!", "No se pudo registrar la venta", "error");
      }
    });
});
