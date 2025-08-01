// Archivo: Apartment.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DTOS
{
    public class Apartment
    {
        int id;
        string door;
        int numberOfRooms;
        double price;
        int floor;
        int numberOfBathrooms;
        double area;
        bool isAvailable;
        string address;
        // Relación con Building
        int idBuilding;

        public Apartment(int id, string address, int floor, string door, int numberOfRooms, double price, int numberOfBathrooms, double area, bool isAvailable,  int idBuilding)
        {
            this.id = id;
            this.door = door;
            this.numberOfRooms = numberOfRooms;
            this.price = price;
            this.floor = floor;
            this.numberOfBathrooms = numberOfBathrooms;
            this.area = area;
            this.isAvailable = isAvailable;
            this.address = address;

            this.idBuilding = idBuilding;
        }

        public int Id { get => id; set => id = value; }
        public string Door { get => door; set => door = value; }
        public int NumberOfRooms { get => numberOfRooms; set => numberOfRooms = value; }
        public double Price { get => price; set => price = value; }
        public int Floor { get => floor; set => floor = value; }
        public int NumberOfBathrooms { get => numberOfBathrooms; set => numberOfBathrooms = value; }
        public double Area { get => area; set => area = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public string Address { get => address; set => address = value; }
        public int IdBuilding { get => idBuilding; set => idBuilding = value; }

        //ToString
        public Apartment(){}
        public override string ToString()
        {
            return $"Apartment ID: {id}, Address: {address}, Floor: {floor}, Door: {door} Rooms: {numberOfRooms}, Price: {price},  Bathrooms: {numberOfBathrooms}, Area: {area} m², Available: {isAvailable}";
        }

        public static async Task ShowApartmentsList() {

            // FRAGMENTO DE CÓDIGO PARA LEER ARCHIVOS

            //string fileRoute = "apartments.json";

            //if (File.Exists(fileRoute))
            //{


            //    // ToDo Hacer que haga un writeline por cada linea del JSON


            //    string apartmentJson = File.ReadAllText(fileRoute);

            //    // Deserializar JSON a objeto Persona
            //    // Deserializar sirve para convertir los datos del JSON a un objeto de C#
            //    Apartment apartment = JsonSerializer.Deserialize<Apartment>(apartmentJson);

            //    // Usar los datos
            //    if (apartment != null)
            //    {
            //        Console.WriteLine($"Nombre: {apartment.ToString()}");
            //    }
            //    else
            //    {
            //        Console.WriteLine("No se pudo deserializar el objeto Apartment desde el JSON.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Archivo JSON no encontrado.");
            //}

            try
            {
                using HttpClient client = new HttpClient();

                string apiUrl = "https://localhost:7195/api/apartments";

                // Realizar una petición GET al endpoint para obtener los apartamentos
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido JSON de la respuesta como string
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    // Convertir el JSON a una lista de objetos Apartment
                    List<Apartment> apartments = JsonSerializer.Deserialize<List<Apartment>>(json, options);

                    if (apartments != null)
                    {
                        foreach (var apartment in apartments)
                        {
                            Console.WriteLine(apartment);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo convertir el json a una lista de apartamentos.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error al llamar a la API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
            }
        }
    }
}
