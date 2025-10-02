using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsCourtOperationDto
    {
        public int DayOfWeek { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
 
        public int IdSportsCourt { get; set; }
        public int? Id { get; set; }
    }
}
