using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   
    public class SportsCourtOperationRepository : GenericRepository<SportsCourtOperation>, ISportsCourtOperationRepository
    {
        public SportsCourtOperationRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<List<SportsCourtOperation>?> GetAllSportsCourtOperationByIdCourt(int id)
        {
            return await _dbContext.SportsCourtOperations.Where(x=>x.IdSportsCourt==id)
                .AsNoTracking().ToListAsync();
        }

    }
    public interface ISportsCourtOperationRepository : IGenericRepository<SportsCourtOperation>
    {
        Task<List<SportsCourtOperation>?> GetAllSportsCourtOperationByIdCourt(int id);
    }
}
