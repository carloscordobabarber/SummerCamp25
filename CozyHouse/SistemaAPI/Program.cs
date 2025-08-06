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

    // Seeding de User
    if (!db.Users.Any())
    {
        var users = new List<User>
        {
            new User { Name = "Juan Pérez", Email = "juan.perez@email.com", Password = "Password1!" },
            new User { Name = "Ana García", Email = "ana.garcia@email.com", Password = "Password2!" },
            new User { Name = "Carlos Ruiz", Email = "carlos.ruiz@email.com", Password = "Password3!" },
            new User { Name = "Lucía Torres", Email = "lucia.torres@email.com", Password = "Password4!" },
            new User { Name = "Marta López", Email = "marta.lopez@email.com", Password = "Password5!" },
            new User { Name = "Pedro Sánchez", Email = "pedro.sanchez@email.com", Password = "Password6!" },
            new User { Name = "Sofía Díaz", Email = "sofia.diaz@email.com", Password = "Password7!" },
            new User { Name = "David Gómez", Email = "david.gomez@email.com", Password = "Password8!" },
            new User { Name = "Elena Martín", Email = "elena.martin@email.com", Password = "Password9!" },
            new User { Name = "Manuel Romero", Email = "manuel.romero@email.com", Password = "Password10!" },
            new User { Name = "Paula Navarro", Email = "paula.navarro@email.com", Password = "Password11!" },
            new User { Name = "Javier Ramos", Email = "javier.ramos@email.com", Password = "Password12!" },
            new User { Name = "Carmen Castro", Email = "carmen.castro@email.com", Password = "Password13!" },
            new User { Name = "Alberto Molina", Email = "alberto.molina@email.com", Password = "Password14!" },
            new User { Name = "Teresa Gil", Email = "teresa.gil@email.com", Password = "Password15!" },
            new User { Name = "Raúl Herrera", Email = "raul.herrera@email.com", Password = "Password16!" }
        };
        db.Users.AddRange(users);
        db.SaveChanges();
    }

    // Seeding de Apartment (ya existente)
    if (!db.Apartments.Any())
    {
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

    // Seeding de Rental
    if (!db.Rentals.Any())
    {
        var users = db.Users.ToList();
        var apartments = db.Apartments.ToList();

        // Rentals de ejemplo (no todos los usuarios tienen rental)
        var rentals = new List<Rental>
        {
            new Rental {
                UserId = users[0].UserId,
                ApartmentId = apartments[0].BuildingId, // según tu corrección
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddMonths(6),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[1].UserId,
                ApartmentId = apartments[1].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-5),
                EndDate = DateTime.UtcNow.AddMonths(12),
                IsConfirmed = false
            },
            new Rental {
                UserId = users[2].UserId,
                ApartmentId = apartments[2].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-20),
                EndDate = DateTime.UtcNow.AddMonths(3),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[3].UserId,
                ApartmentId = apartments[0].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-15),
                EndDate = DateTime.UtcNow.AddMonths(8),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[4].UserId,
                ApartmentId = apartments[1].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-2),
                EndDate = DateTime.UtcNow.AddMonths(4),
                IsConfirmed = false
            },
            new Rental {
                UserId = users[5].UserId,
                ApartmentId = apartments[2].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddMonths(7),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[6].UserId,
                ApartmentId = apartments[0].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-12),
                EndDate = DateTime.UtcNow.AddMonths(5),
                IsConfirmed = false
            },
            new Rental {
                UserId = users[7].UserId,
                ApartmentId = apartments[1].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-3),
                EndDate = DateTime.UtcNow.AddMonths(9),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[8].UserId,
                ApartmentId = apartments[2].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow.AddMonths(2),
                IsConfirmed = false
            },
            new Rental {
                UserId = users[9].UserId,
                ApartmentId = apartments[0].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddMonths(10),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[10].UserId,
                ApartmentId = apartments[1].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-25),
                EndDate = DateTime.UtcNow.AddMonths(11),
                IsConfirmed = true
            },
            new Rental {
                UserId = users[11].UserId,
                ApartmentId = apartments[2].BuildingId,
                StartDate = DateTime.UtcNow.AddDays(-18),
                EndDate = DateTime.UtcNow.AddMonths(6),
                IsConfirmed = false
            }
        };

        db.Rentals.AddRange(rentals);
        db.SaveChanges();
    }

    // Seeding de Logs
    if (!db.Logs.Any())
    {
        var users = db.Users.ToList();
        var logs = new List<Log>
        {
            new Log { ActionPerformed = "POST", User = users[0].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[1].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[2].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[3].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[4].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[5].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[6].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[7].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[8].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[9].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[10].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[11].UserId.ToString(), TableAffected = "users", Description = "Usuario creado" },
            new Log { ActionPerformed = "POST", User = users[0].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[1].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[2].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[3].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[4].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[5].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[6].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[7].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[8].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "POST", User = users[9].UserId.ToString(), TableAffected = "rental", Description = "Nuevo alquiler registrado" },
            new Log { ActionPerformed = "UPDATE", User = users[0].UserId.ToString(), TableAffected = "users", Description = "Cambio de password por usuario" },
            new Log { ActionPerformed = "UPDATE", User = users[1].UserId.ToString(), TableAffected = "users", Description = "Actualización de correo electrónico" },
            new Log { ActionPerformed = "DELETE", User = users[2].UserId.ToString(), TableAffected = "rental", Description = "Alquiler eliminado por usuario" }
        };
        db.Logs.AddRange(logs);
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
