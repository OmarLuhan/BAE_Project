
using AutoMapper;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Controllers
{

    [Route("[controller]")]
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;
        private readonly IVentaService _ventaService;
        private readonly IPedidoService _pedidoService;
        public PlantillaController(IMapper mapper, INegocioService negocioService, IVentaService ventaService,IPedidoService pedidoService)
        {
            _mapper = mapper;
            _negocioService = negocioService;
            _ventaService = ventaService;
            _pedidoService=pedidoService;
        }
        [HttpGet("EnviarClave")]
        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{Request.Scheme}://{this.Request.Host}";
            return View();
        }
        [HttpGet("RestablecerClavePlantilla")]
        public IActionResult RestablecerClavePlantilla(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
        [HttpGet("PdfVenta")]
        public async Task<IActionResult> PdfVenta(string numeroVenta)
        {
            VMVenta vmVenta = _mapper.Map<VMVenta>(await _ventaService.Detalle(numeroVenta));
            VMNegocio? vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());
            VMPDFVenta? modelo = new VMPDFVenta();
            modelo.Negocio = vmNegocio;
            modelo.Venta = vmVenta;
            return View(modelo);
        }
         [HttpGet("PdfPedido")]
        public async Task<IActionResult> PdfPedido(string numeroPedido)
        {
            VMPedido vmPedido = _mapper.Map<VMPedido>(await _pedidoService.Detalle(numeroPedido));
            VMNegocio? vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());
            VMPDFPedido? modelo = new()
            {
                Negocio = vmNegocio,
                Pedido = vmPedido
            };
            return View(modelo);
        }

    }
}