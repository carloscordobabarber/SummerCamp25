namespace DTOS
{
    public class IncidenceDto
    {
        public int Id { get; set; }
        public string Spokesperson { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int IssueType { get; set; }
        public string? AssignedCompany { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ApartmentId { get; set; }
        public int RentalId { get; set; }
        public int TenantId { get; set; }
        public string StatusId { get; set; } = null!;
    }
}