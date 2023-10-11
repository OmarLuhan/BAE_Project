
namespace CapstoneG14.Models.ViewModels
{
    public class VMLibro
    {
        public int IdLibro { get; set; }

        public string? CodigoBarra { get; set; }

        public string? Isbn { get; set; }

        public int? IdEditorial { get; set; }
        public string? NombreEditorial { get; set; }

        public int? IdGenero { get; set; }
        public string? NombreGenero { get; set; }

        public string? Autor { get; set; }

        public string? Titulo { get; set; }

        public int? Pendiente { get; set; }

        public string? UrlImagen { get; set; }

        public string? Precio { get; set; }

        public int? EsActivo { get; set; }
    }
}