
using CozyData;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS: permite solicitudes desde Angular (localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyOrigin() //WithOrigins("http://localhost:4200") // Cambia si usas otra URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Agregar el contexto de base de datos
builder.Services.AddDbContext<ContextDataBase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();