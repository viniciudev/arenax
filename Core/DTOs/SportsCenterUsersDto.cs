using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsCenterUsersDto
    {
       
            public UserDto? User { get; set; }
            public int IdUser { get; set; }
            public SportsCenterDto? SportsCenter { get; set; }
            public int IdSportsCenter { get; set; }
        
    }
}
