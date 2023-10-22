
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface INegocioService
    {
        Task<Negocio> Obtener();
        Task<Negocio> GuardarCambios(Negocio entidad, Stream? logo = null, string nombreLogo = "");
    }
}