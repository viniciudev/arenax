using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CourtEvaluations:BaseModel
    {
        public string? Description { get; set; }
        public int Note { get; set; }
        public SportsCourt SportsCourt { get; set; }
        public int IdSportsCourt { get; set; }
    }
}
