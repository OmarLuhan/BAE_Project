
using System.Globalization;
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IGenericRepository<Libro> _libroRepository;
        public PedidoService(IPedidoRepository pedidoRepository, IGenericRepository<Libro> libroRepository)
        {
            _pedidoRepository = pedidoRepository;
            _libroRepository = libroRepository;
        }
        public async Task<Pedido> ActualizarEstado(string numeroPedido, bool estado)
        {
             try{
                IQueryable<Pedido> query=await _pedidoRepository.Consultar(p=>p.NumeroPedido==numeroPedido)??throw new TaskCanceledException("El pedido no existe");
                Pedido pedido=query.Include(dp=>dp.DetallePedidos).First();
                pedido.Estado=estado;
                pedido.FechaEntrega=DateTime.Now;
                return await _pedidoRepository.ActualizarEstado(pedido);
            }catch{
                throw;
            }
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
                 .Include(dp=>dp.DetallePedidos)
                 .ToList();
            }
        }

        public async Task<List<Libro>> ObtenerLibros(string busqueda)
        {
             IQueryable<Libro> query = await _libroRepository.Consultar(
                l => l.EsActivo == true && string.Concat(
                l.CodigoBarra, l.Titulo, l.Autor).Contains(busqueda)
                );
            return query.Include(e => e.IdEditorialNavigation).Include(g => g.IdGeneroNavigation).ToList();
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