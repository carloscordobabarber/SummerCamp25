namespace DTOS
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string CodeBuilding { get; set; } = null!;
        public string CodeStreet { get; set; } = null!;
        public string? Name { get; set; }
        public string Doorway { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}