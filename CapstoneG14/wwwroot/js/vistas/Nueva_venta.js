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
  $("#btnBuscar").click(function () {
    let documento = $("#txtDocumentoCliente").val().trim();
    if (documento == "") {
      toastr.warning("", "Debe completar el campo RUC");
      return;
    }
    if (isNaN(parseInt(documento))) {
      toastr.warning("", "El campo RUC debe ser numerico");
      return;
    }
    const api_token =
      "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InY0MzcyODgzN0BnbWFpbC5jb20ifQ.qwmIuk4LueCl4XG802VV43uzJGleoAfztlS1n01uyC0";
    $("#btnBuscar").LoadingOverlay("show");
    fetch(
      `https://dniruc.apisperu.com/api/v1/ruc/${documento}?token=${api_token}`
    )
      .then((response) => {
        $("#btnBuscar").LoadingOverlay("hide");
        return response.ok ? response.json() : Promise.reject(response);
      })
      .then((responseJson) => {
        if (responseJson.razonSocial) {
          $("#txtNombreCliente").val(responseJson.razonSocial);
        } else {
          toastr.warning("", "No se encontró la razón social en la respuesta.");
        }
      })
      .catch((error) => {
        toastr.warning("", "El numero ingresado no es un documento RUC: ");
      });
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
  let librosParaVenta = [];
  $("#cboBuscarLibro").on("select2:select", function (e) {
    const data = e.params.data;
    let libroEncontrado = librosParaVenta.filter((l) => l.idLibro == data.id);
    if (libroEncontrado.length > 0) {
      $("#cboBuscarLibro").val("").trigger("change");
      toastr.warning("", "El libro ya fue agregado");
      return false;
    }

    swal(
      {
        title: data.titulo,
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
        let libro = {
          idLibro: data.id,
          autor: data.text,
          tituloLibro: data.titulo,
          editorialLibro: data.editorial,
          generoLibro: data.genero,
          cantidad: parseInt(valor),
          precio: data.precio.toString(),
          total: (parseFloat(valor) * data.precio).toString(),
        };
        librosParaVenta.push(libro);
        mostrarLibro_precios();
        $("#cboBuscarLibro").val("").trigger("change");
        swal.close();
      }
    );
  });

  function mostrarLibro_precios() {
    let total = 0,
      igv = 0,
      subtotal = 0;
    let porcentaje = valorImpuesto / 100;

    $("#tbLibro tbody").html("");
    librosParaVenta.forEach((item) => {
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

    subtotal = total / (1 + porcentaje);
    igv = total - subtotal;

    $("#txtSubTotal").val(subtotal.toFixed(2));
    $("#txtIGV").val(igv.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));
  }
  $(document).on("click", "button.btn-eliminar", function () {
    const _idlibro = $(this).data("idLibro");
    librosParaVenta = librosParaVenta.filter((l) => l.idLibro != _idlibro);
    mostrarLibro_precios();
  });

  $("#btnTerminarVenta").click(function () {
    if ($("#txtNombreCliente").val().trim() == "") {
      toastr.warning("", "no ha ingresado el nombre de ningun cliente");
      return;
    }
    if (librosParaVenta.length < 1) {
      toastr.warning("", "debe ingresar productos");
      return;
    }
    const vmDetalleVenta = librosParaVenta;
    const venta = {
      idTipoDocumentoVenta: $("#cboTipoDocumentoVenta").val(),
      documentoCliente: $("#txtDocumentoCliente").val(),
      nombreCliente: $("#txtNombreCliente").val(),
      subTotal: $("#txtSubTotal").val(),
      impuestoTotal: $("#txtIGV").val(),
      total: $("#txtTotal").val(),
      detalleVenta: vmDetalleVenta,
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
          librosParaVenta = [];
          mostrarLibro_precios();
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
});
