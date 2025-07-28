// Archivo: Apartment.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Dominio
{
    public class Apartment
    {
        int id;
        string address;
        int numberOfRooms;
        double price;
        int floor;
        int numberOfBathrooms;
        double area;
        bool isAvailable;


        public Apartment(int id, string address, int numberOfRooms, double price, int floor, int numberOfBathrooms, double area)
        {
            this.id = id;
            this.address = address;
            this.numberOfRooms = numberOfRooms;
            this.price = price;
            this.floor = floor;
            this.numberOfBathrooms = numberOfBathrooms;
            this.area = area;
            this.isAvailable = true; // Default value for availability
        }

        public int IdProperty { get => id; set => id = value; }
        public string AddressProperty { get => address; set => address = value; }
        public int NumberOfRoomsProperty { get => numberOfRooms; set => numberOfRooms = value; }
        public double PriceProperty { get => price; set => price = value; }
        public int FloorProperty { get => floor; set => floor = value; }
        public int NumberOfBathroomsProperty { get => numberOfBathrooms; set => numberOfBathrooms = value; }
        public double AreaProperty { get => area; set => area = value; }
        public bool IsAvailableProperty { get => isAvailable; set => isAvailable = value; }

        //ToString
        public override string ToString()
        {
            return $"Apartment ID: {id}, Address: {address}, Rooms: {numberOfRooms}, Price: {price}, Floor: {floor}, Bathrooms: {numberOfBathrooms}, Area: {area} m², Available: {isAvailable}";
        }

        public void ShowApartmentsList() {

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


        }
    }
}
