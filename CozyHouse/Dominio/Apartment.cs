// Archivo: Apartment.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Apartment(int id, string address, int numberOfRooms, double price, int floor, int numberOfBathrooms, double area)
        {
            this.id = id;
            this.address = address;
            this.numberOfRooms = numberOfRooms;
            this.price = price;
            this.floor = floor;
            this.numberOfBathrooms = numberOfBathrooms;
            this.area = area;
        }

        public int IdProperty { get => id; set => id = value; }
        public string AddressProperty { get => address; set => address = value; }
        public int NumberOfRoomsProperty { get => numberOfRooms; set => numberOfRooms = value; }
        public double PriceProperty { get => price; set => price = value; }
        public int FloorProperty { get => floor; set => floor = value; }
        public int NumberOfBathroomsProperty { get => numberOfBathrooms; set => numberOfBathrooms = value; }
        public double AreaProperty { get => area; set => area = value; }
    }
}
