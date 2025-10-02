using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
 
    public class CourtEvaluationsRepository : GenericRepository<CourtEvaluations>, ICourtEvaluationsRepository
    {
        public CourtEvaluationsRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<List<CourtEvaluations>?> GetAllCourtEvaluationsByIdCourt(int id)
        {
            return await _dbContext.CourtEvaluations.Where(x=>x.IdSportsCourt==id).AsNoTracking().ToListAsync();
        }
    }
    public interface ICourtEvaluationsRepository : IGenericRepository<CourtEvaluations>
    {
      Task<  List<CourtEvaluations>?> GetAllCourtEvaluationsByIdCourt(int id);
    }
}
