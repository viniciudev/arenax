using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SportsCourtRepository : GenericRepository<SportsCourt>, ISportsCourtRepository
    {
        public SportsCourtRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<SportsCourt?> GetByIdAsync(int sportsCourtId)
        {
            var resp = await _dbContext.SportsCourt.Include(x => x.SportsCourtCategories)
                .ThenInclude(x => x.SportsCategory)
                .Where(x => x.Id == sportsCourtId).AsNoTracking().FirstOrDefaultAsync();
            return resp;
        }
        public async Task<List<SportsCourt>?> GetAllByIdSportsCenter(int tenantID)
        {
            var resp = await (from spc in _dbContext.SportsCourt
                               .Include(x => x.SportsCourtCategories).ThenInclude(x => x.SportsCategory)
                               .Include(x=>x.sportsCourtImages)
                               .Include(x=>x.SportsCourtAppointments).ThenInclude(x=>x.Client)
                              where spc.IdSportsCenter == tenantID
                              select new SportsCourt
                              {
                                  Description = spc.Description,
                                  Name = spc.Name,
                                  IdSportsCenter = spc.IdSportsCenter,
                                  Id = spc.Id,
                                  Price = spc.Price,
                                  SubName = spc.SubName,
                                  SportsCourtCategories = spc.SportsCourtCategories,
                                  sportsCourtImages = spc.sportsCourtImages,
                                  SportsCourtAppointments=spc.SportsCourtAppointments,
                                  
                              }).AsNoTracking().ToListAsync();
            return resp;
        }
    }
    public interface ISportsCourtRepository : IGenericRepository<SportsCourt>
    {
        Task<List<SportsCourt>?> GetAllByIdSportsCenter(int tenantID);
        Task<SportsCourt?> GetByIdAsync(int sportsCourtId);
    }
}
