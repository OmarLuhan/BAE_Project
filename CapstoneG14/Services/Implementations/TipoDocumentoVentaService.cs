using CapstoneG14.Models;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
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