using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCourt:BaseModel
    {
        public required string   Name { get; set; }
        public  string? SubName { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int ?IdSportsCenter { get; set; }
        public SportsCenter SportsCenter { get; set; }
        public ICollection<CourtEvaluations> CourtEvaluations { get; set; } = new List<CourtEvaluations>();
        public ICollection<SportsCourtOperation> SportsCourtOperations { get; set; } = new List<SportsCourtOperation>();
        public ICollection<SportsCourtAppointments> SportsCourtAppointments { get; set; } = new List<SportsCourtAppointments>();
        public ICollection<SportsCourtCategory> SportsCourtCategories { get; set; } = new List<SportsCourtCategory>();
        public ICollection<SportsCourtImage> sportsCourtImages { get; set; } = new List<SportsCourtImage>();

    }
}
