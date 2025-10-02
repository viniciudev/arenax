using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    
    public class SportsCenter:BaseModel
    {
        public required string Name { get; set; }
        public string? Cnpj { get; set; }
        public string? Phone { get; set; }
        public string? UniqueFileName { get; set; }
        public Address ?Address { get; set; }
        public string? Amenities { get; set; }
        public OpeningHours? OpeningHours { get; set; }
        public ICollection<SportsCenterUsers> SportsCenterUsers { get; set; } = new List<SportsCenterUsers>();
        public ICollection<SportsCourt> SportsCourts { get; set; } = new List<SportsCourt>();
       
    }

    public class Address:BaseModel
    {
        public string ?Cep { get; set; }
        public string ?StreetAddress { get; set; }
        public string ?Complement { get; set; }
        public string ?District { get; set; }
        public string ?City { get; set; }
        public string ?Uf { get; set; }
        public string ?State { get; set; }
        public SportsCenter ?SportsCenter { get; set; }
        public int SportsCenterId { get; set; }
    }

    public class OpeningHours:BaseModel
    {
        public SportsCenter ?SportsCenter { get; set; }
        public int SportsCenterId { get; set; }
        public string ?Monday { get; set; }
        public string ?Tuesday { get; set; }
        public string ?Wednesday { get; set; }
        public string ?Thursday { get; set; }
        public string ?Friday { get; set; }
        public string ?Saturday { get; set; }
        public string ?Sunday { get; set; }
    }
}
