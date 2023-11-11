using System;
using System.Collections.Generic;

namespace CapstoneG14.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public string? NumeroPedido { get; set; }

    public int? IdTipoDocumentoPedido { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdTienda { get; set; }

    public decimal Total { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Tiendum? IdTiendaNavigation { get; set; }

    public virtual TipoDocumentoVentum? IdTipoDocumentoPedidoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
