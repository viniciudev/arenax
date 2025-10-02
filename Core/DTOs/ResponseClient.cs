using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseClient
    {
        public int IdSportCenter { get; set; }

        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public int Id { get; set; }
        public string? EmailUser { get; set; }
        public string? NameUser { get; set; }
        public Type? TypeUser { get; set; } = null;
        public int ?IdUser { get; set; }
    }
}
