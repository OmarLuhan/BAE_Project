using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface ITipoDocumentoVentaService
    {
        Task<List<TipoDocumentoVentum>> Listar();
    }
}