const MODELO_BASE = {
    idLibro: 0,
    codigoBarra: "",
    isbn:"",
    autor: "",
    titulo:"",
    idEditorial:0,
    idGenero:0,
    pendiente: 0,
    urlImagen:"",
    precio: 0,
    esActivo: 1
}
let tablaData;
$(document).ready(function () {
    fetch("/Editorial/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboEditorial").append(
                        $("<option>").val(item.idEditorial).text(item.descripcion)
                    )
                })
            }
        });
        fetch("/Genero/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboGenero").append(
                        $("<option>").val(item.idGenero).text(item.descripcion)
                    )
                })
            }
        });

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Libro/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idLibro", "visible": false, "searchable": false },
            {
                "data": "urlImagen", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "codigoBarra" },
            { "data": "isbn" },
            { "data": "autor" },
            { "data": "titulo" },
            { "data": "nombreEditorial" },
            { "data": "nombreGenero" },
            { "data": "pendiente" },
            { "data": "precio" },
            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-danger">Inactivo</span>'
                }

            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Libros',
                exportOptions: {
                    columns: [3, 4, 5, 6,7,8]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});
function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idLibro)
    $("#txtIsbn").val(modelo.isbn)
    $("#txtCodigoBarra").val(modelo.codigoBarra)
    $("#txtAutor").val(modelo.autor)
    $("#txtTitulo").val(modelo.titulo)
    $("#cboEditorial").val(modelo.idEditorial == 0 ? $("#cboEditorial option:First") : modelo.idEditorial)
    $("#cboGenero").val(modelo.idGenero == 0 ? $("#cboGenero option:First") : modelo.idGenero)
    $("#txtPendiente").val(modelo.pendiente)
    $("#txtPrecio").val(modelo.precio)
    $("#cboEstado").val(modelo.esActivo)
    $("#txtImagen").val("")
    $("#imgLibro").attr("src", modelo.urlImagen)

    $("#modalData").modal("show")
}
$("#btnNuevo").click(function () {
    mostrarModal();
});
debugger;
$("#btnGuardar").click(function () {
    const inputs = $("input.input-validar").serializeArray();
    const imputs_sin_valor = inputs.filter((item) => item.value.trim() == "");
    if (imputs_sin_valor.length > 0) {
        const mensaje = `Deve completar el campo:"${imputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${imputs_sin_valor[0].name}"]`).focus();
        return;
    };
    const modelo = structuredClone(MODELO_BASE);
    modelo["idLibro"] = parseInt($("#txtId").val());
    modelo["isbn"] = $("#txtIsbn").val();
    modelo["codigoBarra"] = $("#txtCodigoBarra").val();
    modelo["autor"] = $("#txtAutor").val();
    modelo["titulo"] = $("#txtTitulo").val();
    modelo["idEditorial"] = $("#cboEditorial").val();
    modelo["idGenero"] = $("#cboGenero").val();
    modelo["pendiente"] =parseInt($("#txtPendiente").val());
    modelo["precio"] =parseFloat($("#txtPrecio").val());
    modelo["esActivo"] = $("#cboEstado").val();
    const inputFoto = document.getElementById("txtImagen");
    const formData = new FormData();
    formData.append("imagen", inputFoto.files[0]);
    formData.append("modelo", JSON.stringify(modelo));
    $("#modalData").find("div.modal-content").LoadingOverlay("show");
    if (modelo.idLibro == 0) {
        fetch("/Libro/Crear", {
            method: "POST",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row.add(responseJson.objeto).draw(false);
                $("#modalData").modal("hide");
                swal("Listo!", "Libro creado correctamente", "success");
            } else {
                swal("Error!", responseJson.mensaje, "hubo un problema al crear el libro intente de nuevo");
            }
        });
    } else {
        fetch("/Libro/Editar", {
            method: "PUT",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                filaSeleccionada = null;
                $("#modalData").modal("hide");
                swal("listo!", "el libro fue modificado", "success");
            } else {
                swal("error!", responseJson.mensaje, "hubo un problema al modificar el libro intente de nuevo");
            }
        });

    }
 });

let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }
    const data = tablaData.row(filaSeleccionada).data();
    mostrarModal(data);
});
$("#tbdata tbody").on("click", ".btn-eliminar", function () {
    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }
    const data = tablaData.row(fila).data();
    swal({
        title: `Esta seguro de eliminar este libro?`,
        text: "una vez eliminado no podra recuperar el registro",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        colseOnCancel: true,
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");
                fetch(`/Libro/Eliminar?IdLibro=${data.idLibro}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (responseJson.estado) {
                        tablaData.row(fila).remove().draw(false);
                        swal("listo!", "el libro fue Eliminado", "success");
                    } else {
                        swal("error!", responseJson.mensaje, "error");
                    }
                });
            }
        }
    );
});