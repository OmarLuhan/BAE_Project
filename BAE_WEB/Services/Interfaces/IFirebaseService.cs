namespace BAE_WEB.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task<string> SubirStorage(Stream streamArchivo, string carpetaDestino, string nombreArchivo);
        Task<bool> EliminarStorage(string carpetaDestino, string nombreArchivo);
    }
}