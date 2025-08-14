namespace DTOS
{
    public class ImagesDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public byte[] Photo { get; set; } = null!;
    }
}