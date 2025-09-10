using System;
using System.Collections.Generic;

namespace DTOS
{
    public class UserRentalDetailsDto
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public double ApartmentPrice { get; set; }
        public string ApartmentCode { get; set; } = null!;
        public string ApartmentDoor { get; set; } = null!;
        public int ApartmentFloor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StatusId { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string Portal { get; set; } = null!;
        public int Floor { get; set; }
    public string DistrictName { get; set; } = null!;
    public string? PaymentStatusId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentStatusName { get; set; }
    }
}