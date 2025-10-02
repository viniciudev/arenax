using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCourtCategory:BaseModel
    {
        public int SportsCourtId { get; set; }
        public SportsCourt SportsCourt { get; set; }

        public int SportsCategoryId { get; set; }
        public SportsCategory SportsCategory { get; set; }
    }
}
