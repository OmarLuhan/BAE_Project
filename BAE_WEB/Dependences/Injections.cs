using BAE_WEB.Models;
using BAE_WEB.Repositories.Implementations;
using BAE_WEB.Repositories.Interfaces;
using BAE_WEB.Services.Implementations;
using BAE_WEB.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;


namespace BAE_WEB.Dependences
{
    public static class Injections
    {
        public static void Injection(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BaeContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Context"));
            });
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IVentaRepository, VentaRepository>();
            service.AddScoped<ICorreoService, CorreoService>();
            service.AddScoped<IFirebaseService, FirebaseService>();
            service.AddScoped<IUtilidadesService, UtilidadesService>();
            service.AddScoped<IRolService, RolService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IMenuService, MenuService>();
            service.AddScoped<INegocioService, NegocioService>();
            service.AddScoped<IGeneroService, GeneroService>();
            service.AddScoped<IEditorialService, EditorialService>();
            service.AddScoped<ILibroService, LibroService>();
            service.AddScoped<IVentaService, VentaService>();
            service.AddScoped<ITipoDocumentoVentaService, TipoDocumentoVentaService>();
            service.AddScoped<IDashBoardService, DashBoardService>();
            service.AddScoped<ITiendaService, TiendaService>();
            service.AddScoped<IPedidoRepository, PedidoRepository>();
            service.AddScoped<IPedidoService, PedidoService>();
        }
    }

}