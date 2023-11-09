$(document).ready(function(){
fetch("/Tienda/Lista")
.then(response=>{
return response.ok ? response.json() : Promise.reject(response);
})
.then(responseJson=>{
if(responseJson.data.length>0){
    responseJson.data.forEach((item)=>{
        $("#cboTienda").append(
            $("<option>").val(item.idTienda).text(item.descripcion)
        )
    })
}
})
});