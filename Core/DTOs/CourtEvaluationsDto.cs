using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CourtEvaluationsDto
    {
        public string? Description { get; set; }
        public int Note { get; set; }
        public int IdSportsCourt { get; set; }
        public int Id { get; set; }
    }
}
