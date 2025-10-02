using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class ClientEvaluationEvalutionRepository
    {
    }
    public class ClientEvaluationRepository : GenericRepository<ClientEvaluation>, IClientEvaluationRepository
    {
        public ClientEvaluationRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<PlayerMediaResponse> GetAllByIdClient(int id)
        {
            var resp = await _dbContext.ClientEvaluation
                .Include(x=>x.SportsCourtAppointments).ThenInclude(x=>x.SportsCourt)
                .Where(x => x.IdClient == id)
                
                .AsNoTracking()
                .ToListAsync();
            return new PlayerMediaResponse
            {
                Average= resp.Count>0?(decimal)resp.Average(x=>x.Note):0,
                ClientEvaluations=resp
            };
        }
    }
    public interface IClientEvaluationRepository : IGenericRepository<ClientEvaluation>
    {
        Task<PlayerMediaResponse> GetAllByIdClient(int id);
    }
}
