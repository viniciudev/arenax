namespace Core.DTOs
{
    public class SportCourtDto
    {

        public required string Name { get; set; }
        public string? SubName { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int? IdSportsCenter { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();

    }
}
