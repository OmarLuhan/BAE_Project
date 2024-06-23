using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface INegocioService
    {
        Task<Negocio> Obtener();
        Task<Negocio> GuardarCambios(Negocio entidad, Stream? logo = null, string nombreLogo = "");
    }
}