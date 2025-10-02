using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SportsCourtAppointments : BaseModel
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int ?IdClient { get; set; }
        public Client Client { get; set; }
        public SchedulingStatus? Status { get; set; }
        public int IdSportsCourt { get; set; }
        public SportsCourt? SportsCourt { get; set; }
        //ligado ao dono do horário
        public decimal Payment { get; set; }
        public int Number { get; set; }
        public decimal Value { get; set; }
        public bool OpenPublic { get; set; } 
        public string? PaymentMethod { get; set; }
        public decimal Price { get; set; }
        public int? IdCategory { get; set; }
        public SportsCategory? SportsCategory { get; set; }
        public ICollection<SportsRegistration> SportsRegistrations { get; set; } = new List<SportsRegistration>();
        public ICollection<ClientEvaluation> ClientEvaluations { get; set; }=new List<ClientEvaluation>();
    }

    public enum SchedulingStatus
    {
        available=1,//disponível
        scheduled=2,//agendado
        pending=3,//pendente
        canceled=4, //cancelado
    }
}
