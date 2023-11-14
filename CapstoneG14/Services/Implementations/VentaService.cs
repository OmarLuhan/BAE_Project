
using System.Globalization;
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class VentaService : IVentaService
    {
        private readonly IGenericRepository<Libro> _libroRepository;
        private readonly IVentaRepository _ventaRepository;
        public VentaService(IGenericRepository<Libro> libroRepository, IVentaRepository ventaRepository)
        {
            _libroRepository = libroRepository;
            _ventaRepository = ventaRepository;
        }
        public async Task<Ventum> Detalle(string numeroVenta)
        {
            IQueryable<Ventum> query = await _ventaRepository.Consultar(v => v.NumeroVenta == numeroVenta);
            return query
            .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
            .Include(u => u.IdUsuarioNavigation)
            .Include(dv => dv.DetalleVenta)
            .First();
        }

        public async Task<List<Ventum>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Ventum> query = await _ventaRepository.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
            if (fechaInicio != "" && fechaFin != "")
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));
                return query.Where(
                    v => v.FechaRegistro.Value.Date >= fecha_inicio.Date
                    && v.FechaRegistro.Value.Date <= fecha_fin.Date
                ).Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .Include(dv => dv.DetalleVenta)
                .ToList();
            }
            else
            {
                return query.Where(v => v.NumeroVenta == numeroVenta
                  ).Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
                 .Include(u => u.IdUsuarioNavigation)
                 .Include(dv => dv.DetalleVenta)
                 .ToList();

            }
        }

        public async Task<List<Libro>> ObtenerLibros(string busqueda)
        {
            IQueryable<Libro> query = await _libroRepository.Consultar(
                l => l.EsActivo == true && l.Stock > 0 && string.Concat(
                l.CodigoBarra, l.Titulo, l.Autor).Contains(busqueda)
                );
            return query.Include(e => e.IdEditorialNavigation).Include(g => g.IdGeneroNavigation).ToList();
        }

        public async Task<Ventum> Registrar(Ventum venta)
        {
            try
            {
                return await _ventaRepository.Registrar(venta);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<DetalleVentum>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));
            List<DetalleVentum> lista = await _ventaRepository.Reporte(fecha_inicio, fecha_fin);
            return lista;
        }
    }
}