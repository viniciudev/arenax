using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsCenterLogoDto
    {
        public int Id { get; set; }
        public required IFormFile Logo { get; set; }
    }
}
