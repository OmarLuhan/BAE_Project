using BAE_WEB.Models;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Repositories.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<Pedido> Registrar(Pedido pedido);
        Task<List<DetallePedido>> Reporte(DateTime fechaInicio, DateTime fechaFin);
        Task<Pedido> ActualizarEstado(Pedido pedido);
    }
}