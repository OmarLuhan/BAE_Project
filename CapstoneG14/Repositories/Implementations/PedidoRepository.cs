
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Repositories.Implementations
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly BaeContext _context;
        public PedidoRepository(BaeContext context) : base(context)
        {
            _context = context;
        }
        public Task<Pedido> ActualizarEstado(int idPedido, bool estado)
        {
            throw new NotImplementedException();
        }

        public async Task<Pedido> Registrar(Pedido pedido)
        {
            Pedido pedidoGenerado=new();
            using(var transaction=_context.Database.BeginTransaction()){
                try{
                    if(pedido.Estado==true){
                        foreach(DetallePedido dp in pedido.DetallePedidos){
                        Libro libroEncontrado=await _context.Libros.Where(l=>l.IdLibro==dp.IdLibro).FirstAsync();
                        libroEncontrado.Pendiente+=dp.Cantidad;
                        _context.Libros.Update(libroEncontrado);
                        await _context.SaveChangesAsync();
                    }
                    }
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
                }catch(Exception ex){
                    transaction.Rollback();
                    throw ex;
                }
            }
            return pedidoGenerado;
        }

        public Task<List<DetallePedido>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {
           throw new NotImplementedException();
        }
    }
}