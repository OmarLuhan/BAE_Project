using CapstoneG14.Dependences;
using CapstoneG14.Utilities.AutoMapper;
using CapstoneG14.Utilities.Extenciones;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddMvcOptions(options =>
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true
        })
    );
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
builder.Services.Injection(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// #region DinkToPdf
// // aqui se inyecta el assembly
// var context = new CustomAssemblyLoadContext();
// context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Utilities/LibreriaPDF/libwkhtmltox.dll"));
// builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
// #endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
