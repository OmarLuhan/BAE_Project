namespace BAE_WEB.Services.Interfaces
{
    public interface ICorreoService
    {
        public Task<bool> EnviarCorreo(string correo, string asunto, string mensaje);
    }
}