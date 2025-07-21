using Microsoft.EntityFrameworkCore;
using VotacionBLL.Services.Partido;
using VotacionBLL.Services.Voto;
using VotacionBLL.Services.Votante;
using VotacionDAL;
using VotacionDAL.Repositories.Partido;
using VotacionDAL.Repositories.Voto;
using VotacionDAL.Repositories.Votante;
using VotacionObjetos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar HttpClient para inyección en repositorios o servicios
builder.Services.AddHttpClient();


builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapeoClases>());

// Configurar DbContext
builder.Services.AddDbContext<VotacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("VotacionDAL")));

// Registrar repositorios
builder.Services.AddScoped<IPartidoRepository, PartidoRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IVotanteRepository, VotanteRepository>();

// Registrar servicios
builder.Services.AddScoped<IPartidoService, PartidoService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IVotanteService, VotanteService>();

// Configurar CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirMVC", policy =>
    {
        policy.WithOrigins("https://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirMVC");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
