using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsRegistration : BaseModel
    {

        public int IdSportsCourtAppointments { get; set; }
        public SportsCourtAppointments SportsCourtAppointments { get; set; }
        public int IdClient { get; set; }
        public Client Client { get; set; }
        [NotMapped] 
        public double? AverageRating { get; set; }
        public StatusSportsRegistration Status { get; set; }
    }
    public enum StatusSportsRegistration
    {
        pending=1,
        approved=2,
        rejected=3
    }
}
