using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class SportsCourtImageDto
    {
        public int Id { get; set; }
        public required List<IFormFile> Images { get; set; }

    }
}
