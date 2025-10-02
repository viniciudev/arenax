using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCourtOperation:BaseModel
    {
        public int DayOfWeek { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SportsCourt SportsCourt { get; set; }
        public int IdSportsCourt { get; set; }
    }
}
