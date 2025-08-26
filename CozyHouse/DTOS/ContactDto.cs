namespace DTOS
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string ContactReason { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}