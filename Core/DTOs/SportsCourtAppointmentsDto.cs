using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SportsCourtAppointmentsDto
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int IdSportsCourt { get; set; }
        public SchedulingStatus? Status { get; set; }
        public int Id { get; set; }
        public decimal Payment { get; set; }
        public int? IdClient { get; set; }
        public int Number { get; set; }
        public decimal Value { get; set; }
        public bool OpenPublic { get; set; } = false;
        public string? PaymentMethod { get; set; }
        public decimal Price { get; set; }
        public int? IdCategory { get; set; }
    }
}
