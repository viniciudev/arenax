using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportCenterFilter
    {
        public string? Name { get; set; }
        public string? Cep { get; set; }
        public string? StreetAddress { get; set; }
        public string? Complement { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Uf { get; set; }
        public string? State { get; set; }
    }
}
