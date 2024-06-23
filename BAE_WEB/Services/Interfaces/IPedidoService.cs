using System.Runtime.InteropServices;
using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> Registrar(Pedido pedido);
        Task<Pedido> Detalle(string numeroPedido);
        Task<List<Pedido>> Historial(string numeroPedido, string fechaInicio, string fechaFin);
        Task<Pedido> ActualizarEstado(string numeroPedido, bool estado);
        Task<List<Libro>> ObtenerLibros(string busqueda);
    }
}