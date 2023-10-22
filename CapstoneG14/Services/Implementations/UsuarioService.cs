
using System.Net;
using Microsoft.EntityFrameworkCore;
using CapstoneG14.Models;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repositorio;
        private readonly IFirebaseService _firebaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;
        public UsuarioService(IGenericRepository<Usuario> repositorio, IFirebaseService firebaseService, IUtilidadesService utilidadesService, ICorreoService correoService)
        {
            _repositorio = repositorio;
            _firebaseService = firebaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }
        public async Task<bool> CambiarClave(int idUauario, string claveActual, string claveNueva)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == idUauario) ?? throw new TaskCanceledException("No se encontro el usuario");
                if (usuario_encontrado.Clave != _utilidadesService.ConvertirSha256(claveActual))
                    throw new TaskCanceledException("La contraseña ingresada no coincide con la contraseña actual");

                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(claveNueva);
                bool respuesta = await _repositorio.Update(usuario_encontrado);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Usuario> Crear(Usuario usuario, Stream foto = null, string nombreFoto = "", string urlPlantillaCorreo = "")
        {
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == usuario.Correo);
            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");
            try
            {
                string claveGenerada = _utilidadesService.GenerarClave();
                usuario.Clave = _utilidadesService.ConvertirSha256(claveGenerada);
                usuario.NombreFoto = nombreFoto;
                if (foto != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(foto, "carpeta_usuario", nombreFoto);
                    usuario.UrlFoto = urlFoto;
                }
                Usuario usuario_creado = await _repositorio.Create(usuario);
                if (usuario_creado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");
                if (urlPlantillaCorreo != "")
                {
                    urlPlantillaCorreo = urlPlantillaCorreo.Replace("[correo]", usuario_creado.Correo).Replace("[clave]", claveGenerada);
                    string htmlCorreo = "";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader? readerStream = null;
                            if (response.CharacterSet == null)
                                readerStream = new StreamReader(dataStream);
                            else
                                readerStream = new StreamReader(dataStream, System.Text.Encoding.GetEncoding(response.CharacterSet));
                            htmlCorreo = readerStream.ReadToEnd();
                            readerStream.Close();
                            response.Close();
                        }
                    }
                    if (htmlCorreo != "")
                        await _correoService.EnviarCorreo(usuario_creado.Correo, "Cuenta creada Bienvenido a store_code", htmlCorreo);
                }
                IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == usuario_creado.IdUsuario);
                usuario_creado = query.Include(r => r.IdRolNavigation).First();
                return usuario_creado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Usuario> Editar(Usuario usuario, Stream? foto = null, string nombreFoto = "")
        {
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == usuario.Correo && u.IdUsuario != usuario.IdUsuario);
            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");
            try
            {
                IQueryable<Usuario> queryUsuario = await _repositorio.Consultar(u => u.IdUsuario == usuario.IdUsuario);
                Usuario usuario_editar = queryUsuario.First();
                usuario_editar.Nombre = usuario.Nombre;
                usuario_editar.Correo = usuario.Correo;
                usuario_editar.Telefono = usuario.Telefono;
                usuario_editar.IdRol = usuario.IdRol;
                usuario_editar.EsActivo = usuario.EsActivo;

                if (usuario_editar.NombreFoto == "")
                    usuario_editar.NombreFoto = nombreFoto;
                if (foto != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(foto, "carpeta_usuario", usuario_editar.NombreFoto);
                    usuario_editar.UrlFoto = urlFoto;
                }

                bool respuesta = await _repositorio.Update(usuario_editar);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el usuario");
                Usuario usuario_editado = queryUsuario.Include(r => r.IdRolNavigation).First();
                return usuario_editado;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idUsuario)
        {
            try
            {
                Usuario usuario_encrontado = await _repositorio.Obtener(u => u.IdUsuario == idUsuario);
                if (usuario_encrontado == null)
                    throw new TaskCanceledException("No se encontro el usuario");
                string? nombreFoto = usuario_encrontado.NombreFoto;
                bool respuesta = await _repositorio.Delete(usuario_encrontado);
                if (respuesta)
                    await _firebaseService.EliminarStorage("carpeta_usuario", nombreFoto);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> GuardarPerfil(Usuario usuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == usuario.IdUsuario);
                if (usuario_encontrado == null)
                    throw new TaskCanceledException("No se encontro el usuario");
                usuario_encontrado.Correo = usuario.Correo;
                usuario_encontrado.Telefono = usuario.Telefono;
                bool respuesta = await _repositorio.Update(usuario_encontrado);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Usuario>> Lista()
        {
            IQueryable<Usuario> query = await _repositorio.Consultar();
            return query.Include(r => r.IdRolNavigation).ToList();
        }

        public async Task<Usuario> ObtenerPorCredenciales(string correo, string clave)
        {
            string claveEncriptada = _utilidadesService.ConvertirSha256(clave);
            Usuario usuarioEncontrado = await _repositorio.Obtener(u => u.Correo.Equals(correo) && u.Clave.Equals(claveEncriptada));
            return usuarioEncontrado;
        }

        public async Task<Usuario> ObtenerPorId(int idUsuario)
        {
            IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == idUsuario);
            Usuario usuario = query.Include(r => r.IdRolNavigation).FirstOrDefault();
            return usuario;
        }

        public async Task<bool> RecuperarClave(string correo, string urlPlantillaCorreo)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo == correo) ?? throw new TaskCanceledException("No encontramos ningun usuario asociado al correo");
                string claveGenerada = _utilidadesService.GenerarClave();
                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(claveGenerada);

                urlPlantillaCorreo = urlPlantillaCorreo.Replace("[Clave]", claveGenerada);
                string htmlCorreo = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader readerStream = null;
                        if (response.CharacterSet == null)
                            readerStream = new StreamReader(dataStream);
                        else
                            readerStream = new StreamReader(dataStream, System.Text.Encoding.GetEncoding(response.CharacterSet));
                        htmlCorreo = readerStream.ReadToEnd();
                        readerStream.Close();
                        response.Close();
                    }
                }
                bool correo_enviado = false;
                if (htmlCorreo != "")
                    correo_enviado = await _correoService.EnviarCorreo(correo, "Contraseña restablecida", htmlCorreo);
                if (!correo_enviado)
                    throw new TaskCanceledException("Tenemos problemas: profavor intentalo mas tarde");
                bool respuesta = await _repositorio.Update(usuario_encontrado);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}