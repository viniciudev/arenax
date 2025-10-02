using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
 
    public class SportsCourtCategoryRepository : GenericRepository<SportsCourtCategory>, ISportsCourtCategoryRepository
    {
        public SportsCourtCategoryRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        //public async Task<List<SportsCourtCategory>?> GetAllSportsCourtCategoryByIdCourt(int id)
        //{
        //    var resp = await _dbContext.SportsCourtCategory.Where(x => x.IdSportsCourt == id).AsNoTracking().ToListAsync();
        //    return resp;
        //}

    }
    public interface ISportsCourtCategoryRepository : IGenericRepository<SportsCourtCategory>
    {
        //Task<List<SportsCourtCategory>?> GetAllSportsCourtCategoryByIdCourt(int id);
    }
}
