using Firebase.Auth;
using Firebase.Storage;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;
using BAE_WEB.Services.Interfaces;
using BAE_WEB.Models;

namespace BAE_WEB.Services.Implementations
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IGenericRepository<Configuracion> _Service;
        public FirebaseService(IGenericRepository<Configuracion> Service)
        {
            _Service = Service;
        }
        public async Task<bool> EliminarStorage(string carpetaDestino, string nombreArchivo)
        {
            try
            {
                IQueryable<Configuracion> query = await _Service.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["email"], config["clave"]);
                var cancelationToken = new CancellationTokenSource();
                var task = new FirebaseStorage(config["ruta"], new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child(config[carpetaDestino]).Child(nombreArchivo).DeleteAsync();
                await task;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<string> SubirStorage(Stream streamArchivo, string carpetaDestino, string nombreArchivo)
        {
            string urlImagen = "";
            try
            {
                IQueryable<Configuracion> query = await _Service.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["email"], config["clave"]);
                var cancelationToken = new CancellationTokenSource();
                var task = new FirebaseStorage(config["ruta"], new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child(config[carpetaDestino]).Child(nombreArchivo).PutAsync(streamArchivo, cancelationToken.Token);
                urlImagen = await task;
            }
            catch
            {
                urlImagen = "";
            }
            return urlImagen;
        }
    }
}