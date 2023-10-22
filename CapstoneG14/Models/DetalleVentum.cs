using System;
using System.Collections.Generic;

namespace CapstoneG14.Models;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdLibro { get; set; }

    public string? EditorialLibro { get; set; }

    public string? TituloLibro { get; set; }

    public string? GeneroLibro { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
