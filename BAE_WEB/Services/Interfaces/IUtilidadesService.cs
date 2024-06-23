namespace BAE_WEB.Services.Interfaces
{
    public interface IUtilidadesService
    {
        string GenerarClave();
        string ConvertirSha256(string input);
    }
}