using System.Globalization;
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IGenericRepository<DetalleVentum> _detalleVentaRepository;
        private readonly IGenericRepository<Libro> _libroRepository;
        private readonly DateTime _fechaInicio = DateTime.Now;
        public DashBoardService(IVentaRepository ventaRepository, IGenericRepository<DetalleVentum> detalleVentaRepository, IGenericRepository<Libro> libroRepository, IPedidoRepository pedidoRepository)
        {
            _ventaRepository = ventaRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _libroRepository = libroRepository;
            _fechaInicio = _fechaInicio.AddDays(-7);
            _pedidoRepository = pedidoRepository;
        }


        public async Task<Dictionary<string, int>> LibrosTopUltimaSemana()
        {
            try
            {
                IQueryable<DetalleVentum> query = await _detalleVentaRepository.Consultar();
                Dictionary<string, int> resultado = query
                .Include(v => v.IdVentaNavigation)
                .Where(v => v.IdVentaNavigation.FechaRegistro.Value.Date >= _fechaInicio.Date)
                .GroupBy(dv => dv.TituloLibro)
                .OrderByDescending(g => g.Count())
                .Select(dv => new { libro = dv.Key, total = dv.Count() }).Take(4)
                .ToDictionary(keySelector: r => r.libro, elementSelector: r => r.total);
                return resultado;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> TotalIngresosUltimaSemana()
        {
            try
            {
                IQueryable<Ventum> query = await _ventaRepository.Consultar(v => v.FechaRegistro.Value.Date >= _fechaInicio.Date);
                decimal resultado = query.Select(v => v.Total).Sum(v => v.Value);
                return Convert.ToString(resultado, new CultureInfo("es-PE"));
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalLibros()
        {
            try
            {
                IQueryable<Libro> query = await _libroRepository.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> PedidosRestantes()
        {
            try{
                IQueryable<Pedido> query = await _pedidoRepository.Consultar(p => p.Estado == false);
                int total = query.Count();
                return total;
            }catch{
                throw;
            }
        }

        public async Task<int> TotalVentasUltimaSemana()
        {
            try
            {
                IQueryable<Ventum> query = await _ventaRepository.Consultar(v => v.FechaRegistro.Value.Date >= _fechaInicio.Date);
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            try
            {
                IQueryable<Ventum> query = await _ventaRepository.Consultar(v => v.FechaRegistro.Value.Date >= _fechaInicio.Date);
                Dictionary<string, int> resultado = query.GroupBy(v => v.FechaRegistro.Value.Date)
                .OrderByDescending(g => g.Key)
                .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
                return resultado;
            }
            catch
            {
                throw;
            }
        }
    }
}