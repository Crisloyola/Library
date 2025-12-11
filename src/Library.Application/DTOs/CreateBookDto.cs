namespace Library.Application.DTOs
{
    public class CreateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}