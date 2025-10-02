namespace Core
{
    public class Client : BaseModel
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public ICollection<SportsCourtAppointments> SportsCourtAppointments { get; set; } =new List<SportsCourtAppointments>();
        public User ?User { get; set; }
        public ICollection<SportsRegistration> SportsRegistrations { get; set; } = new List<SportsRegistration>();
        public ICollection<ClientEvaluation> ClientEvaluations { get; set; } = new List<ClientEvaluation>();
        public string? TokenApp { get; set; }
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();
    }
}
