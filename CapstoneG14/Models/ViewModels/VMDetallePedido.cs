
namespace CapstoneG14.Models.ViewModels
{
    public class VMDetallePedido
    {
         public int IdLibro { get; set; }

        public string? EditorialLibro { get; set; }

        public string? TituloLibro { get; set; }

        public string? GeneroLibro { get; set; }

        public int? Cantidad { get; set; }

        public string? Precio { get; set; }

        public string? Total { get; set; }
    }
}