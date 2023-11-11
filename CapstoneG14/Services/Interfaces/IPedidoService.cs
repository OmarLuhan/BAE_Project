using System.Runtime.InteropServices;
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> Registrar(Pedido pedido);
        Task<Pedido>Detalle(string numeroPedido);
        Task<List<Pedido>> Historial(string numeroPedido, string fechaInicio, string fechaFin);
    }
}