
using Microsoft.EntityFrameworkCore;
using CapstoneG14.Models;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
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