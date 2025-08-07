// Archivo: Apartment.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DTOS
{
    public class ApartmentDTO
    {
        //int id;
        //string door;
        //int numberOfRooms;
        //double price;
        //int floor;
        //int numberOfBathrooms;
        //double area;
        //bool isAvailable;
        //string address;
        //// Relación con Building
        //int idBuilding;

        //public ApartmentDTO(int id, string address, int floor, string door, int numberOfRooms, double price, int numberOfBathrooms, double area, bool isAvailable,  int idBuilding)
        //{
        //    this.id = id;
        //    this.door = door;
        //    this.numberOfRooms = numberOfRooms;
        //    this.price = price;
        //    this.floor = floor;
        //    this.numberOfBathrooms = numberOfBathrooms;
        //    this.area = area;
        //    this.isAvailable = isAvailable;
        //    this.address = address;

        //    this.idBuilding = idBuilding;
        //}

        public int Id { get; set; }
        public string Door { get; set; }
        public int NumberOfRooms { get; set; }
        public double Price { get; set; }
        public int Floor { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }
        public int BuildingId { get; set; }

        //ToString
        //public ApartmentDTO(){}
        //public override string ToString()
        //{
        //    return $"Apartment ID: {id}, Address: {address}, Floor: {floor}, Door: {door} Rooms: {numberOfRooms}, Price: {price},  Bathrooms: {numberOfBathrooms}, Area: {area} m², Available: {isAvailable}";
        //}

        //public static async Task ShowApartmentsList() {

         
        //    try
        //    {
        //        using HttpClient client = new HttpClient();

        //        string apiUrl = "https://localhost:7195/api/apartments";

        //        // Realizar una petición GET al endpoint para obtener los apartamentos
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
                
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Leer el contenido JSON de la respuesta como string
        //            string json = await response.Content.ReadAsStringAsync();

        //            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        //            // Convertir el JSON a una lista de objetos Apartment
        //            List<ApartmentDTO> apartments = JsonSerializer.Deserialize<List<ApartmentDTO>>(json, options);

        //            if (apartments != null)
        //            {
        //                foreach (var apartment in apartments)
        //                {
        //                    Console.WriteLine(apartment);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("No se pudo convertir el json a una lista de apartamentos.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Error al llamar a la API: {response.StatusCode}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Excepción: {ex.Message}");
        //    }
        //}
    }
}
