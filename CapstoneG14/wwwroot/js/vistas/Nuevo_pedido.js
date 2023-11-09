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
});
