using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Apartment
    {
        int Id;
        string Address;
        int NumberOfRooms;
        double Price;
        int Floor;
        int NumberOfBathrooms;
        double Area;

        public Apartment(int id, string address, int numberOfRooms, double price, int floor, int numberOfBathrooms, double area)
        {
            this.Id = id;
            this.Address = address;
            this.NumberOfRooms = numberOfRooms;
            this.Price = price;
            this.Floor = floor;
            this.NumberOfBathrooms = numberOfBathrooms;
            this.Area = area;
        }

        public int IdProperty { get => Id; set => Id = value; }
        public string AddressProperty { get => Address; set => Address = value; }
        public int NumberOfRoomsProperty { get => NumberOfRooms; set => NumberOfRooms = value; }
        public double PriceProperty { get => Price; set => Price = value; }
        public int FloorProperty { get => Floor; set => Floor = value; }
        public int NumberOfBathroomsProperty { get => NumberOfBathrooms; set => NumberOfBathrooms = value; }
        public double AreaProperty { get => Area; set => Area = value; }
    }
}