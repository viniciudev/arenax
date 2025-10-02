namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;

        public IUserRepository User { get; }
        public ICourtEvaluationsRepository CourtEvaluations { get; }
        public ISportsCourtOperationRepository SportsCourtOperation { get; }
        public ISportsCategoryRepository SportsCategory { get; }
        public ISportsCourtRepository SportsCourt { get; }
        public ISportsCourtAppointmentsRepository SportsCourtAppointments { get; }
        public ISportsCenterRepository SportsCenter { get; }
        public ISportsCenterUsersRepository SportsCenterUsers { get; }
        public ISportsCourtCategoryRepository SportsCourtCategory { get; }
        public IClientRepository Client { get; }
        public ISportsCourtImageRepository SportsCourtImage { get; }
        public ISportsRegistrationRepository SportsRegistration { get; }
        public INotificationsRepository Notifications { get; }
        public IClientEvaluationRepository ClientEvaluation { get; }
        public IOtpRepository Otp { get; }
        public UnitOfWork(
            DbContextClass dbContext,
            IUserRepository userRepository,
            ISportsCourtRepository sportsCourtRepository,
            ISportsCourtOperationRepository sportsCourtOperationRepository,
            ISportsCategoryRepository sportsCategoryRepository,
            ICourtEvaluationsRepository courtEvaluationsRepository,
            ISportsCourtAppointmentsRepository sportsCourtAppointmentsRepository,
            ISportsCenterRepository sportsCenterRepository,
            ISportsCenterUsersRepository sportsCenterUsers,
            ISportsCourtCategoryRepository sportsCourtCategoryRepository,
            IClientRepository clientRepository,
            ISportsCourtImageRepository sportsCourtImageRepository,
            ISportsRegistrationRepository SportsRegistrationsRepository,
            INotificationsRepository NotificationsRepository,
            IClientEvaluationRepository clientEvaluationRepository,
            IOtpRepository otpRepository
            )
        {
            _dbContext = dbContext;
            User = userRepository;
            CourtEvaluations = courtEvaluationsRepository;
            SportsCourt = sportsCourtRepository;
            SportsCategory = sportsCategoryRepository;
            SportsCourtOperation = sportsCourtOperationRepository;
            SportsCourtAppointments = sportsCourtAppointmentsRepository;
            SportsCenter = sportsCenterRepository;
            SportsCenterUsers = sportsCenterUsers;
            SportsCourtCategory = sportsCourtCategoryRepository;
            Client = clientRepository;
            SportsCourtImage = sportsCourtImageRepository;
            SportsRegistration = SportsRegistrationsRepository;
            Notifications = NotificationsRepository;
            ClientEvaluation = clientEvaluationRepository;
            Otp = otpRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        ICourtEvaluationsRepository CourtEvaluations { get; }
        ISportsCategoryRepository SportsCategory { get; }
        ISportsCourtRepository SportsCourt { get; }
        ISportsCourtOperationRepository SportsCourtOperation { get; }
        ISportsCourtAppointmentsRepository SportsCourtAppointments { get; }
        ISportsCenterRepository SportsCenter { get; }
        ISportsCenterUsersRepository SportsCenterUsers { get; }
        ISportsCourtCategoryRepository SportsCourtCategory { get; }
        IClientRepository Client { get; }
        ISportsCourtImageRepository SportsCourtImage { get; }
        ISportsRegistrationRepository SportsRegistration { get; }
        INotificationsRepository Notifications { get; }
        IClientEvaluationRepository ClientEvaluation { get; }
        IOtpRepository Otp { get; }
        int Save();
    }
}
