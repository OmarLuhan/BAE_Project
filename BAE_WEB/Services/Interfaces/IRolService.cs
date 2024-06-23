using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> Lista();
    }
}