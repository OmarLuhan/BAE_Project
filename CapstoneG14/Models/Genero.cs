using System;
using System.Collections.Generic;

namespace CapstoneG14.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
