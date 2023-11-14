using System;
using System.Collections.Generic;

namespace CapstoneG14.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Isbn { get; set; }

    public string? Titulo { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public string? Autor { get; set; }

    public string? UrlImagen { get; set; }

    public string? NombreImagen { get; set; }

    public int? IdGenero { get; set; }

    public int? IdEditorial { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }
}
