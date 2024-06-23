using System;
using System.Collections.Generic;

namespace BAE_WEB.Models;

public partial class Editorial
{
    public int IdEditorial { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
