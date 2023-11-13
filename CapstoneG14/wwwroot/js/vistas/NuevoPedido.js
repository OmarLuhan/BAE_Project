$(document).ready(function () {
  fetch("/Tienda/Lista")
    .then((response) => {
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.data.length > 0) {
        responseJson.data.forEach((item) => {
          $("#cboTienda").append(
            $("<option>").val(item.idTienda).text(item.descripcion)
          );
        });
      }
    });
  fetch("/Venta/ListaTipoDocumentoVenta")
    .then((response) => {
      return response.ok ? response.json() : Promise.reject(response);
    })
    .then((responseJson) => {
      if (responseJson.length > 0) {
        responseJson.forEach((item) => {
          $("#cboTipoDocumento").append(
            $("<option>").val(item.idTipoDocumentoVenta).text(item.descripcion)
          );
        });
      }
    });
  $("#cboBuscarLibro").select2({
    ajax: {
      url: "/Venta/ObtenerLibros",
      dataType: "json",
      constentType: "application/json; charset=utf-8",
      delay: 200,
      data: function (params) {
        return {
          busqueda: params.term,
        };
      },
      processResults: function (data) {
        return {
          results: data.map((item) => ({
            id: item.idLibro,
            text: item.autor,
            titulo: item.titulo,
            editorial: item.nombreEditorial,
            genero: item.nombreGenero,
            urlImagen: item.urlImagen,
            precio: parseFloat(item.precio),
          })),
        };
      },
    },
    language: "es",
    placeholder: "Buscar Libro...",
    minimumInputLength: 1,
    templateResult: formatRepo,
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
                     <p style="font-weight:bolder;margin:2px"> ${data.titulo}</p>
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
  let librosParaPedido = [];
  $("#cboBuscarLibro").on("select2:select", function (e) {
    const data = e.params.data;
    let libroEncontrado = librosParaPedido.filter((l) => l.idLibro == data.id);
    if (libroEncontrado.length > 0) {
      $("#cboBuscarLibro").val("").trigger("change");
      toastr.warning("", "El libro ya fue agregado");
      return false;
    }

    Swal.fire({
      imageUrl: data.urlImagen,
      title: data.titulo,
      text: data.text,
      imageWidth: 80,
      imageHeight: 120,
      html: `
          <div class="form-group col-sm-12">
          <div class="form-group">
          <input id="input-Cantidad" class="form-control col-sm-12" placeholder="Cantidad">
          </div>
          <div class="form-group">
          <input id="input-Precio" class="form-control col-sm-12" placeholder="Precio">
          </div>
          </div>
          `,
      showCancelButton: true,
      focusConfirm: false,
      preConfirm: () => {
        let cantidad = document.getElementById("input-Cantidad").value;
        let precio = document.getElementById("input-Precio").value;
        if (!cantidad || !precio) {
          Swal.showValidationMessage(
            "Ambos campos, cantidad y precio, son requeridos"
          );
          return false;
        }
        if (isNaN(parseInt(cantidad)) || isNaN(parseFloat(precio))) {
          Swal.showValidationMessage(
            "Ambos campos, cantidad y precio, deben ser numéricos"
          );
          return false;
        }
        return { cantidad: cantidad, precio: precio };
      },
    }).then((result) => {
      if (result.isConfirmed) {
        let libro = {
          idLibro: data.id,
          autor: data.text,
          tituloLibro: data.titulo,
          editorialLibro: data.editorial,
          generoLibro: data.genero,
          cantidad: parseInt(result.value.cantidad),
          precio: result.value.precio.toString(),
          total: (
            parseFloat(result.value.cantidad) * parseFloat(result.value.precio)
          )
            .toFixed(2)
            .toString(),
        };
        librosParaPedido.push(libro);
        mostrarLibro_precios();
      }
    });
  });
  function mostrarLibro_precios() {
    let total = 0;
    $("#tbLibro tbody").html("");
    librosParaPedido.forEach((item) => {
      total += parseFloat(item.total);
      $("#tbLibro tbody").append(
        $("<tr>").append(
          $("<td>").append(
            $("<button>")
              .addClass("btn btn-danger btn-eliminar btn-sm")
              .append($("<i>").addClass("fa fa-trash-alt"))
              .data("idLibro", item.idLibro)
          ),
          $("<td>").text(item.tituloLibro),
          $("<td>").text(item.cantidad),
          $("<td>").text(item.precio),
          $("<td>").text(item.total)
        )
      );
    });
    $("#txtTotal").val(total.toFixed(2));
  }
  $(document).on("click", "button.btn-eliminar", function () {
    const _idlibro = $(this).data("idLibro");
    librosParaPedido = librosParaPedido.filter((l) => l.idLibro != _idlibro);
    mostrarLibro_precios();
  });

  $("#btnTerminarPedido").click(function () {
    if ($("#cboTienda").val() == "0") {
      toastr.warning("", "Debe seleccionar una Tienda");
      $("#cboTienda").focus();
      return;
    }
    if (librosParaPedido.length < 1) {
      toastr.warning("", "debe ingresar productos");
      return;
    }
    if ($("#txtFechaEntrega").val() == "") {
      toastr.warning("", "debe ingresar una fecha de entrega");
      return;
    }
    const vmDetallePedido = librosParaPedido;
    const modelo = {
      idTipoDocumentoPedido: parseInt($("#cboTipoDocumento").val()),
      idTienda: parseInt($("#cboTienda").val()),
      estado: parseInt($("#cboEstado").val()),
      total: $("#txtTotal").val(),
      DetallePedidos: vmDetallePedido,
    };
    debugger;
    $("#btnTerminarPedido").LoadingOverlay("show");
    console.log(modelo);
    fetch("/Pedido/RegistrarPedido", {
      method: "POST",
      headers: { "Content-Type": "application/json;charset=utf-8" },
      body: JSON.stringify(modelo), // Convertir el objeto a una cadena JSON antes de enviarlo
    })
      .then((response) => {
        $("#btnTerminarPedido").LoadingOverlay("hide");
        return response.ok ? response.json() : Promise.reject(response);
      })
      .then((responseJson) => {
        if (responseJson.estado) {
          librosParaPedido = [];
          mostrarLibro_precios();
          $("#cboTienda").val($("#cboTienda option:first").val());
          $("#cboTipoDocumento").val($("#cboTipoDocumento option:first").val());
          swal.fire(
            "Registrado!",
            `Numero Pedido: ${responseJson.objeto.numeroPedido}`,
            "success"
          );
        } else {
          swal.fire(
            "Lo sentimos!",
            "No se pudo registrar la el pedido intentelo nuevamente",
            "error"
          );
        }
      });
  });
});
