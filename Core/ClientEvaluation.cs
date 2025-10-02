using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClientEvaluation:BaseModel
    {
        public string? Description { get; set; }
        public int Note { get; set; }
        public Client Client { get; set; }
        public int IdClient { get; set; }
        public int? IdAppointments { get; set; }
        public SportsCourtAppointments SportsCourtAppointments { get; set; }
    }
}
