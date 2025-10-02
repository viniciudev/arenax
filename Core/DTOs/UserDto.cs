using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Type Type { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? IdClient { get; set; }
    }
}
