
using Microsoft.EntityFrameworkCore;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;
using BAE_WEB.Services.Interfaces;
using BAE_WEB.Models;

namespace BAE_WEB.Services.Implementations
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol>? _rolRepository;
        public RolService(IGenericRepository<Rol>? rolRepository)
        {
            _rolRepository = rolRepository;
        }
        public async Task<List<Rol>> Lista()
        {
            IQueryable<Rol> query = await _rolRepository.Consultar();
            List<Rol> roles = await query.ToListAsync();
            return roles;
        }
    }
}