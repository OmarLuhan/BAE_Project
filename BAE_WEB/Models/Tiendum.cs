using System;
using System.Collections.Generic;

namespace BAE_WEB.Models;

public partial class Tiendum
{
    public int IdTienda { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
