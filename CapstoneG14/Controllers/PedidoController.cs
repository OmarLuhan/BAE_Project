using System.Security.Claims;
using AutoMapper;
using CapstoneG14.Models;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;
using CapstoneG14.Utilities.CustomFilter;
using CapstoneG14.Utilities.Response;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Controllers
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

        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService, IMapper mapper, IConverter converter, ITipoDocumentoVentaService tipoDocumentoVentaService)
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
        [HttpPost("RegistrarPedido")]
        public async Task<IActionResult> RegistrarPedido([FromBody]VMPedido modelo)
        {
            GenericResponse<VMPedido> gResponse = new();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .SingleOrDefault();
                modelo.IdUsuario = int.Parse(idUsuario);

                Pedido predido_creado= await _pedidoService.Registrar(_mapper.Map<Pedido>(modelo));
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
        [HttpGet("Historial")]
        public async Task<IActionResult> Historial(string numeroPedido, string fechaInicio = "", string fechaFin = "")
        {
            List<VMPedido> vmHistorialPedido = _mapper.Map<List<VMPedido>>(await _pedidoService.Historial(numeroPedido, fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, vmHistorialPedido);
        }

    }
}