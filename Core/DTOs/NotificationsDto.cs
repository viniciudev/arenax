namespace Core.DTOs
{
    public class NotificationsDto
    {

        public required string Title { get; set; }
        public required string Body { get; set; }
        public bool Read { get; set; } = false;
        public int? IdClient { get; set; }
        public int Id { get; set; }
    }
}
