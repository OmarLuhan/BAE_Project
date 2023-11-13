

namespace CapstoneG14.Models.ViewModels
{
    public class VMPedido
    {
    public int IdPedido { get; set; }
    public string? NumeroPedido { get; set; }
    public int? IdTipoDocumentoPedido { get; set; }
    public string? TipoDocumentoPedido { get; set; }
    public int? IdUsuario { get; set; }
    public string? Usuario { get; set; }
    public int? IdTienda { get; set; }
    public string? Tienda { get; set; }
    public string? Total { get; set; }
    public int ? Estado { get; set; }
    public string? FechaRegistro { get; set; }
    public string? FechaEntrega { get; set; }

    public virtual ICollection<VMDetallePedido> DetallePedidos { get; set; }
    }
}