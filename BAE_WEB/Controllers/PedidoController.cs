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
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoService _pedidoService;
        private readonly ITipoDocumentoVentaService _tipoDocumentoVentaService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService, IMapper mapper,IConverter converter,ITipoDocumentoVentaService tipoDocumentoVentaService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
            _mapper = mapper;
            _converter = converter;
            _tipoDocumentoVentaService = tipoDocumentoVentaService;
        }
        [ClaimRequirement("Pedido", "NuevoPedido")]
        [HttpGet("NuevoPedido")]
        public IActionResult NuevoPedido()
        {
            return View();
        }
        [ClaimRequirement("pedido", "HistorialPedido")]
        [HttpGet("HistorialPedido")]
        public IActionResult HistorialPedido()
        {
            return View();
        }
        [HttpGet("ObtenerLibros")]
        public async Task<IActionResult> ObtenerLibros(string busqueda)
        {
            List<VMLibro> vmListaLibros = _mapper.Map<List<VMLibro>>(await _pedidoService.ObtenerLibros(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaLibros);
        }
        [HttpPost("RegistrarPedido")]
        public async Task<IActionResult> RegistrarPedido([FromBody] VMPedido modelo)
        {
            GenericResponse<VMPedido> gResponse = new();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .SingleOrDefault();
                modelo.IdUsuario = int.Parse(idUsuario);

                Pedido predido_creado = await _pedidoService.Registrar(_mapper.Map<Pedido>(modelo));
                modelo = _mapper.Map<VMPedido>(predido_creado);
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
        [HttpPut("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstado(string numeroPedido, int estado)
        {
            GenericResponse<VMPedido> gResponse = new();
            try
            {
                Pedido pedido_editado = await _pedidoService.ActualizarEstado(numeroPedido, Convert.ToBoolean(estado));
                VMPedido modelo = _mapper.Map<VMPedido>(pedido_editado);
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
        public async Task<IActionResult> Historial(string numeroPedido, string fechaInicio = "", string fechaFin = "")
        {
            List<VMPedido> vmHistorialPedido = _mapper.Map<List<VMPedido>>(await _pedidoService.Historial(numeroPedido, fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, vmHistorialPedido);
        }
        [HttpGet("MostrarPdfPedido")]
        public IActionResult MostrarPdfPedido(string numeroPedido)
        {
            string urlPlantillaVista = $"{Request.Scheme}://{this.Request.Host}/Plantilla/PdfPedido?numeroPedido={numeroPedido}";
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects ={
                     new ObjectSettings()
                     {
                         Page= urlPlantillaVista
                     }
                 }
            };
            var archivoPdf = _converter.Convert(pdf);
            return File(archivoPdf, "application/pdf");
        }

    }
}