using BAE_WEB.Models;
using BAE_WEB.Services.Interfaces;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Services.Implementations
{
    public class TipoDocumentoVentaService : ITipoDocumentoVentaService
    {
        private readonly IGenericRepository<TipoDocumentoVentum> _repository;
        public TipoDocumentoVentaService(IGenericRepository<TipoDocumentoVentum> repository)
        {
            _repository = repository;
        }
        public async Task<List<TipoDocumentoVentum>> Listar()
        {
            IQueryable<TipoDocumentoVentum> query = await _repository.Consultar();
            return query.ToList();
        }
    }
}