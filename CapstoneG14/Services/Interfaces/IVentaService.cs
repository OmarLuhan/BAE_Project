using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface IVentaService
    {
        Task<List<Libro>> ObtenerLibros(string busqueda);
        Task<Ventum> Registrar(Ventum venta);
        Task<List<Ventum>> Historial(string numeroVenta, string fechaInicio, string fechaFin);
        Task<Ventum> Detalle(string numeroVenta);
        Task<List<DetalleVentum>> Reporte(string fechaInicio, string fechaFin);
    }
}