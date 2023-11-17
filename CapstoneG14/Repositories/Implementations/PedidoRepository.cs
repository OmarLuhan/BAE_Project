using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapstoneG14.Repositories.Implementations
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly BaeContext _context;
        public PedidoRepository(BaeContext context) : base(context)
        {
            _context = context;
        }
        

        public async Task<Pedido> Registrar(Pedido pedido)
        {
            Pedido pedidoGenerado=new();
            using(var transaction=_context.Database.BeginTransaction()){
                try{
                    NumeroCorrelativo correlative=await _context.NumeroCorrelativos.Where(nc=>nc.Gestion=="Pedido").FirstAsync();
                    correlative.UltimoNumero+=1;
                    correlative.FechaActualizacion=DateTime.Now;
                    _context.NumeroCorrelativos.Update(correlative);
                    await _context.SaveChangesAsync();
                    string ceros=string.Concat(Enumerable.Repeat("0",correlative.CantidadDigitos.Value));
                    string numeroPedido=ceros+correlative.UltimoNumero.ToString();
                    numeroPedido=numeroPedido.Substring(numeroPedido.Length-correlative.CantidadDigitos.Value,correlative.CantidadDigitos.Value);
                    pedido.NumeroPedido=numeroPedido;
                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync();
                    pedidoGenerado=pedido;
                    transaction.Commit();
                }catch(Exception){
                    transaction.Rollback();
                    throw;
                }
            }
            return pedidoGenerado;
        }

        public async Task<List<DetallePedido>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {
           List<DetallePedido> reporte= await _context.DetallePedidos
           .Include(p=>p.IdPedidoNavigation).ThenInclude(u=>u.IdUsuarioNavigation)
           .Include(p=>p.IdPedidoNavigation).ThenInclude(tdp=>tdp.IdTipoDocumentoPedidoNavigation)
           .Include(p=>p.IdPedidoNavigation).ThenInclude(t=>t.IdTiendaNavigation)
           .Where(dp=>dp.IdPedidoNavigation.FechaRegistro.Value.Date>=fechaInicio.Date &&
              dp.IdPedidoNavigation.FechaRegistro.Value.Date<=fechaFin.Date).ToListAsync();
            return reporte;
        }
        public async Task<Pedido> ActualizarEstado(Pedido pedido)
        {
            Pedido pedidoEditado=new();
            using(var transaction=_context.Database.BeginTransaction()){
                try{
                    if(pedido.Estado){
                     foreach(DetallePedido dp in pedido.DetallePedidos){
                        Libro libroEncontrado=await _context.Libros.Where(l=>l.IdLibro==dp.IdLibro).FirstAsync();
                        libroEncontrado.Stock+=dp.Cantidad;
                        _context.Libros.Update(libroEncontrado);
                        await _context.SaveChangesAsync();
                        }
                    }
                    _context.Pedidos.Update(pedido);
                    await _context.SaveChangesAsync();
                    pedidoEditado=pedido;
                    transaction.Commit();
                }catch(Exception){
                    transaction.Rollback();
                    throw;
                }
            }
          return pedidoEditado;
        }
    }
}