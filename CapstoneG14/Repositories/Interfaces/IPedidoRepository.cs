using CapstoneG14.Models;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Repositories.Interfaces
{
    public interface IPedidoRepository:IGenericRepository<Pedido>
    {
        Task<Pedido> Registrar(Pedido pedido);
        Task<List<DetallePedido>> Reporte(DateTime fechaInicio, DateTime fechaFin);
        Task<Pedido>ActualizarEstado(int idPedido, bool estado);
    }
}