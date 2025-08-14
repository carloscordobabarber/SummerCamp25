namespace DTOS
{
    public class ImagesDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string? PhotoDescription { get; set; }
    }
}