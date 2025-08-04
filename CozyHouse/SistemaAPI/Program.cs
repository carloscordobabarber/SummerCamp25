using CozyData;
using Microsoft.EntityFrameworkCore;
using Dominio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar el contexto de base de datos
builder.Services.AddDbContext<ContextoAPI>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seeding de datos de Apartment
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContextoAPI>();
    if (!db.Apartments.Any())
    {
        // Si no hay edificios, crea uno para la relación
        var building = db.Buildings.FirstOrDefault();
        if (building == null)
        {
            building = new Building { Name = "Edificio Central", Address = "Calle Principal 123" };
            db.Buildings.Add(building);
            db.SaveChanges();
        }

        db.Apartments.AddRange(
            new Apartment {
                Door = "A", Floor = 1, Price = 120000, Area = 80, NumberOfRooms = 3, NumberOfBathrooms = 2, IsAvailable = true, BuildingId = building.BuildingId
            },
            new Apartment {
                Door = "B", Floor = 2, Price = 95000, Area = 65, NumberOfRooms = 2, NumberOfBathrooms = 1, IsAvailable = true, BuildingId = building.BuildingId
            },
            new Apartment {
                Door = "C", Floor = 3, Price = 150000, Area = 100, NumberOfRooms = 4, NumberOfBathrooms = 2, IsAvailable = false, BuildingId = building.BuildingId
            }
        );
        db.SaveChanges();
    }
}

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
