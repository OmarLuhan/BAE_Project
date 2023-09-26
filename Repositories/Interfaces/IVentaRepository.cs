using CapstoneG14.Models;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Repositories.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Ventum>
    {
        Task<Ventum> Registrar(Ventum ventum);
        Task<List<DetalleVentum>> Reporte(DateTime fechaInicio, DateTime fechaFin);
    }
}