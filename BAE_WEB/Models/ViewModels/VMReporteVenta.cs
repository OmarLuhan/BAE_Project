namespace BAE_WEB.Models.ViewModels
{
    public class VMReporteVenta
    {
        public string? FechaReigstro { get; set; }
        public string? NumeroVenta { get; set; }
        public string? TipoDocumento { get; set; }
        public string? TipoDocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? SubtotalVenta { get; set; }
        public string? ImpuestoTotalVenta { get; set; }
        public string? TotalVenta { get; set; }
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Libro { get; set; }
        public string? Total { get; set; }
    }
}