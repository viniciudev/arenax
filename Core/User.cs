using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User:BaseModel
    {
        public Type Type { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? UniqueFileName { get; set; }
        public ICollection<SportsCenterUsers> SportsCenterUsers { get; set; } = new List<SportsCenterUsers>();
        public int? IdClient { get; set; }
        public Client? Client { get; set; }

    }
    public enum Type
    {
        Quadra=1,
        Jogador=2
    }
}
