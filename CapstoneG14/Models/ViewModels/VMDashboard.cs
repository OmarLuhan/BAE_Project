

namespace CapstoneG14.Models.ViewModels
{
    public class VMDashboard
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int TotalLibros { get; set; }
        public int PedidosRestantes { get; set; }
        public List<VMVentasSemana> VentasUltimaSemana { get; set; }
        public List<VMLibrosSemana> LibrosTopUltimaSemana { get; set; }
    }
}