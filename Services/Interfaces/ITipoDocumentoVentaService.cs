
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface ITipoDocumentoVentaService
    {
        Task<List<TipoDocumentoVentum>> Listar();
    }
}