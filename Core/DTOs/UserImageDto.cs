using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserImageDto
    {
        public int Id { get; set; }
        public required IFormFile Image { get; set; }
    }
}
