using System;
using System.Collections.Generic;

namespace CapstoneG14.Models;

public partial class DetallePedido
{
    public int IdDetallePedido { get; set; }

    public int? IdPedido { get; set; }

    public int? IdLibro { get; set; }

    public string? EditorialLibro { get; set; }

    public string? TituloLibro { get; set; }

    public string? GeneroLibro { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
