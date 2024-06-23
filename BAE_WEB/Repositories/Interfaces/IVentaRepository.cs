using BAE_WEB.Models;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Repositories.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Ventum>
    {
        Task<Ventum> Registrar(Ventum ventum);
        Task<List<DetalleVentum>> Reporte(DateTime fechaInicio, DateTime fechaFin);
    }
}