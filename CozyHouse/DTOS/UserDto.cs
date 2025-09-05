namespace DTOS
{
    public class UserDto
    {
        public int Id { get; set; }
        public string DocumentType { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } = null!;
    }
}