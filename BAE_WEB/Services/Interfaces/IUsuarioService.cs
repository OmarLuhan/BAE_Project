using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> Lista();
        Task<Usuario> Crear(Usuario usuario, Stream? foto = null, string nombreFoto = "", string urlPlantillaCorreo = "");
        Task<Usuario> Editar(Usuario usuario, Stream? foto = null, string nombreFoto = "");
        Task<bool> Eliminar(int idUsuario);
        Task<Usuario> ObtenerPorCredenciales(string? correo, string? clave);
        Task<Usuario> ObtenerPorId(int idUsuario);
        Task<bool> GuardarPerfil(Usuario usuario);
        Task<bool> CambiarClave(int idUauario, string claveActual, string claveNueva);
        Task<bool> RecuperarClave(string correo, string urlPlantillaCorreo);
    }
}