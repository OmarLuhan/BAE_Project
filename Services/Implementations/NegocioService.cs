

using CapstoneG14.Models;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class NegocioService : INegocioService
    {
        private readonly IGenericRepository<Negocio> _negocioRepository;
        private readonly IFirebaseService _fireBaseService;
        public NegocioService(IGenericRepository<Negocio> negocioRepository, IFirebaseService fireBaseService)
        {
            _negocioRepository = negocioRepository;
            _fireBaseService = fireBaseService;
        }
        public async Task<Negocio> GuardarCambios(Negocio entidad, Stream? logo = null, string nombreLogo = "")
        {
            try
            {
                Negocio negocio_encontrado = await _negocioRepository.Obtener(n => n.IdNegocio == 1);
                negocio_encontrado.NumeroDocumento = entidad.NumeroDocumento;
                negocio_encontrado.Nombre = entidad.Nombre;
                negocio_encontrado.Correo = entidad.Correo;
                negocio_encontrado.Direccion = entidad.Direccion;
                negocio_encontrado.Telefono = entidad.Telefono;
                negocio_encontrado.PorcentajeImpuesto = entidad.PorcentajeImpuesto;
                negocio_encontrado.SimboloMoneda = entidad.SimboloMoneda;
                negocio_encontrado.NombreLogo = negocio_encontrado.NombreLogo == "" ? nombreLogo : negocio_encontrado.NombreLogo;
                if (logo != null)
                {
                    string? url_logo = await _fireBaseService.SubirStorage(logo, "carpeta_logo", negocio_encontrado.NombreLogo);
                    negocio_encontrado.UrlLogo = url_logo;

                }

                await _negocioRepository.Update(negocio_encontrado);
                return negocio_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Negocio> Obtener()
        {
            try
            {
                Negocio negocio_encontrado = await _negocioRepository.Obtener(n => n.IdNegocio == 1);
                return negocio_encontrado;

            }
            catch
            {
                throw;
            }
        }
    }
}