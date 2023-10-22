namespace CapstoneG14.Services.Interfaces
{
    public interface IUtilidadesService
    {
        string GenerarClave();
        string ConvertirSha256(string input);
    }
}