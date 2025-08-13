namespace DTOS
{
    public class LogDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ActionPerformed { get; set; } = null!;
        public int UsersId { get; set; }
        public string TableAffected { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}