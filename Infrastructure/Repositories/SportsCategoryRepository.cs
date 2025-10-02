using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class SportsCategoryRepository : GenericRepository<SportsCategory>, ISportsCategoryRepository
    {
        public SportsCategoryRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        //public async Task<List<SportsCategory>?> GetAllSportsCategoryByIdCourt(int id)
        //{
        //    var resp = await _dbContext.SportsCategory.Where(x => x.IdSportsCourt == id).AsNoTracking().ToListAsync();
        //    return resp;
        //}

        public async Task<List<SportsCategory>?> GetAll()
        {
            var resp = await _dbContext.SportsCategory.AsNoTracking().ToListAsync();
               return resp;
        }

    }
    public interface ISportsCategoryRepository : IGenericRepository<SportsCategory>
    {
        //Task<List<SportsCategory>?> GetAllSportsCategoryByIdCourt(int id);
        Task<List<SportsCategory>?> GetAll();
    }
}
