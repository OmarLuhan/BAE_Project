namespace BAE_WEB.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<int> TotalVentasUltimaSemana();
        Task<string> TotalIngresosUltimaSemana();
        Task<int> TotalLibros();
        Task<int> PedidosRestantes();
        Task<Dictionary<string, int>> VentasUltimaSemana();
        Task<Dictionary<string, int>> LibrosTopUltimaSemana();

    }
}