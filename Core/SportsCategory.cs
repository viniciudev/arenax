using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCategory:BaseModel
    {
        public required string Description { get; set; }
        public string ?SvgContent { get; set; }
        public ICollection<SportsCourtCategory> SportsCourtCategories { get; set; } = new List<SportsCourtCategory>();
        public ICollection<SportsCourtAppointments> SportsCourtAppointments { get; set; } = new List<SportsCourtAppointments>();
    }
}
