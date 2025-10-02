using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCenterUsers:BaseModel
    {
        public User? User { get; set; }
        public int IdUser { get; set; }
        public SportsCenter? SportsCenter { get; set; }
        public int IdSportsCenter { get; set; }
    }
}
