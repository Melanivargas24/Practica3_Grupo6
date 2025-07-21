using Microsoft.EntityFrameworkCore;
using VotacionDAL;
using VotacionBLL.Services.Partido;
using VotacionBLL.Services.Votante;
using VotacionBLL.Services.Voto;
using VotacionDAL.Repositories.Partido;
using VotacionDAL.Repositories.Votante;
using VotacionDAL.Repositories.Voto;
using VotacionObjetos;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Configurar DbContext
builder.Services.AddDbContext<VotacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias - Repositorios
builder.Services.AddScoped<IPartidoRepository, PartidoRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IVotanteRepository, VotanteRepository>();

// Inyección de dependencias - Servicios
builder.Services.AddScoped<IPartidoService, PartidoService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IVotanteService, VotanteService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapeoClases>());

builder.Services.AddHttpClient();

var app = builder.Build();


// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
