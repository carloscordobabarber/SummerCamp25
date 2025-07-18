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
        public int Id { get => id; set => id = value; }


    }
}