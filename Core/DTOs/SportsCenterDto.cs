using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsCenterDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Cnpj { get; set; }
        public string? Phone { get; set; }
        public List<string> ?Amenities { get; set; }
        public AddressDto? Address { get; set; }
        public OpeningHoursDto? OpeningHours { get; set; }
        public required int IdUser { get; set; }
        public string? UniqueFileName { get; set; }
    }

    public class AddressDto
    {
        public string ?Cep { get; set; }
        public string ?StreetAddress { get; set; }
        public string ?Complement { get; set; }
        public string ?District { get; set; }
        public string ?City { get; set; }
        public string ?Uf { get; set; }
        public string ?State { get; set; }
    }

    public class OpeningHoursDto
    {
        public string ?Monday { get; set; }
        public string ?Tuesday { get; set; }
        public string ?Wednesday { get; set; }
        public string ?Thursday { get; set; }
        public string ?Friday { get; set; }
        public string ?Saturday { get; set; }
        public string ?Sunday { get; set; }
    }
}
