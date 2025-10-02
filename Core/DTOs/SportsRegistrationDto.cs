using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsRegistrationDto
    {
        public int Id { get; set; }
        public int IdSportsCourtAppointments { get; set; }

        public int IdClient { get; set; }

        public StatusSportsRegistration Status { get; set; }
    }
}
