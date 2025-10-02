using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class SportsRegistrationRepository : GenericRepository<SportsRegistration>, ISportsRegistrationRepository
    {
        public SportsRegistrationRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<List<SportsRegistration>?> GetAllByIdClient(int id)
        {
            var result = await _dbContext.SportsRegistration
             .Include(x => x.SportsCourtAppointments)
                 .ThenInclude(x => x.SportsCourt)
                 .ThenInclude(a => a.SportsCenter)
                 .ThenInclude(x => x.Address)

             .Include(x => x.SportsCourtAppointments)
                 .ThenInclude(x => x.Client)

             .Include(x => x.SportsCourtAppointments)
                 .ThenInclude(x => x.SportsCategory)

             .Include(x => x.Client)
                 .ThenInclude(x => x.ClientEvaluations)

             .Where(x => x.IdClient == id)
             .OrderByDescending(x => x.Id)
             .AsNoTracking()
             .ToListAsync();

            // Calcular a média para cada registro
            foreach (var registration in result)
            {
                if (registration.Client?.ClientEvaluations != null &&
                    registration.Client.ClientEvaluations.Any())
                {
                    double average = registration.Client.ClientEvaluations
                        .Average(ce => ce.Note);

                     registration.AverageRating = average;
                }
            }

            return result;
        }
        public async Task<List<SportsRegistration>?> GetAllSportsRegistrations(int id)
        {
            var result = await _dbContext.SportsRegistration.
                Include(x => x.SportsCourtAppointments)
                .Include(x => x.Client).       
                ThenInclude(x => x.ClientEvaluations)
                 .Where(x => x.IdSportsCourtAppointments == id)
                 .AsNoTracking().ToListAsync();
            // Calcular a média para cada registro
            foreach (var registration in result)
            {
                if (registration.Client?.ClientEvaluations != null &&
                    registration.Client.ClientEvaluations.Any())
                {
                    double average = registration.Client.ClientEvaluations
                        .Average(ce => ce.Note);

                    registration.AverageRating = average;
                }
            }
            return result;
        }
    }
    public interface ISportsRegistrationRepository : IGenericRepository<SportsRegistration>
    {
        Task<List<SportsRegistration>?> GetAllByIdClient(int id);
        Task<List<SportsRegistration>?> GetAllSportsRegistrations(int id);
    }
}
