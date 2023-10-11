using CapstoneG14.Models;
using CapstoneG14.Repositories.Implementations;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Implementations;
using CapstoneG14.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;


namespace CapstoneG14.Dependences
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
        }
    }

}