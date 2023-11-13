
using System.Globalization;
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapstoneG14.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<Pedido> Detalle(string numeroPedido)
        {
            IQueryable<Pedido> query = await _pedidoRepository.Consultar(p => p.NumeroPedido == numeroPedido);
            return query.Include(t=>t.IdTipoDocumentoPedidoNavigation)
            .Include(u=>u.IdUsuarioNavigation)
            .Include(t=>t.IdTiendaNavigation)
            .Include(dp=>dp.DetallePedidos)
            .First();
        }
        public async Task<List<Pedido>> Historial(string numeroPedido, string fechaInicio, string fechaFin)
        {
            IQueryable<Pedido> query =await  _pedidoRepository.Consultar();
            fechaInicio=fechaInicio is null ? "" : fechaInicio;
            fechaFin=fechaFin is null ? "" : fechaFin;
            if(fechaInicio!="" && fechaFin!=""){
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));
                return query.Where(
                    p => p.FechaRegistro.Value.Date >= fecha_inicio.Date
                    && p.FechaRegistro.Value.Date <= fecha_fin.Date
                ).Include(t=>t.IdTipoDocumentoPedidoNavigation)
                .Include(u=>u.IdUsuarioNavigation)
                .Include(t=>t.IdTiendaNavigation)
                .Include(dp=>dp.DetallePedidos)
                .ToList();
            }
            else{
                return query.Where(p => p.NumeroPedido == numeroPedido
                  ).Include(t=>t.IdTipoDocumentoPedidoNavigation)
                 .Include(u=>u.IdUsuarioNavigation)
                 .Include(t=>t.IdTiendaNavigation)
                 .ToList();
            }
        }

        public async Task<Pedido>Registrar(Pedido pedido)
        {
           try{
                return await _pedidoRepository.Registrar(pedido);
           }catch{
            
               throw;
           } 
        }
    }
}