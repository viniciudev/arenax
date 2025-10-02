namespace Core.DTOs
{
    public class ClientDto
    {
     
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public int Id { get; set; }
        public string? TokenApp { get; set; }
    }
}
