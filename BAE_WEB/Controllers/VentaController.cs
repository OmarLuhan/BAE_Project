
using System.Security.Claims;
using AutoMapper;
using BAE_WEB.Models;
using BAE_WEB.Models.ViewModels;
using BAE_WEB.Services.Interfaces;
using BAE_WEB.Utils.CustomFilter;
using BAE_WEB.Utils.Response;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BAE_WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;
        private readonly ITipoDocumentoVentaService _tipoDocumentoVentaService;
        private readonly IMapper _mapper;
        // private readonly IConverter _converter;
        private readonly ILogger<VentaController> _logger;

        public VentaController(ITipoDocumentoVentaService tipoDocumentoVentaService, IMapper mapper, IVentaService ventaService, /*IConverter converter,*/ ILogger<VentaController> logger)
        {
            _ventaService = ventaService;
            _tipoDocumentoVentaService = tipoDocumentoVentaService;
            _mapper = mapper;
            //  _converter = converter;
            _logger = logger;
        }
        [ClaimRequirement("Venta", "NuevaVenta")]
        [HttpGet("NuevaVenta")]
        public IActionResult NuevaVenta()
        {
            return View();
        }
        [ClaimRequirement("Venta", "HistorialVenta")]
        [HttpGet("HistorialVenta")]
        public IActionResult HistorialVenta()
        {
            return View();
        }
        [HttpGet("ListaTipoDocumentoVenta")]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoVenta> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumentoVenta>>(await _tipoDocumentoVentaService.Listar());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }
        [HttpGet("ObtenerLibros")]
        public async Task<IActionResult> ObtenerLibros(string busqueda)
        {
            List<VMLibro> vmListaLibros = _mapper.Map<List<VMLibro>>(await _ventaService.ObtenerLibros(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaLibros);
        }
        [HttpPost("RegistrarVenta")]
        public async Task<IActionResult> RegistrarVenta([FromBody] VMVenta modelo)
        {
            GenericResponse<VMVenta> gResponse = new();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .SingleOrDefault();
                modelo.IdUsuario = int.Parse(idUsuario);

                Ventum venta_creada = await _ventaService.Registrar(_mapper.Map<Ventum>(modelo));
                modelo = _mapper.Map<VMVenta>(venta_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception e)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = e.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpGet("Historial")]
        public async Task<IActionResult> Historial(string numeroVenta, string fechaInicio = "", string fechaFin = "")
        {
            List<VMVenta> vmHistorialVenta = _mapper.Map<List<VMVenta>>(await _ventaService.Historial(numeroVenta, fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, vmHistorialVenta);
        }
        // [HttpGet("MostrarPdfVenta")]
        // public IActionResult MostrarPdfVenta(string numeroVenta)
        // {
        //     string urlPlantillaVista = $"{Request.Scheme}://{this.Request.Host}/Plantilla/PdfVenta?numeroVenta={numeroVenta}";
        //     var pdf = new HtmlToPdfDocument()
        //     {
        //         GlobalSettings = new GlobalSettings()
        //         {
        //             PaperSize = PaperKind.A4,
        //             Orientation = Orientation.Portrait
        //         },
        //         Objects ={
        //             new ObjectSettings()
        //             {
        //                 Page= urlPlantillaVista
        //             }
        //         }
        //     };
        //     var archivoPdf = _converter.Convert(pdf);
        //     return File(archivoPdf, "application/pdf");
        // }

    }

}