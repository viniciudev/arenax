using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
 
    public class SportsCourtImageRepository : GenericRepository<SportsCourtImage>, ISportsCourtImageRepository
    {
        public SportsCourtImageRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        //public async Task<List<SportsCourtImage>?> GetAllSportsCourtImageByIdCourt(int id)
        //{
        //    var resp = await _dbContext.SportsCourtImage.Where(x => x.IdSportsCourt == id).AsNoTracking().ToListAsync();
        //    return resp;
        //}
        public async Task<SportsCourtImage?> GetByName(string filename)
        {
            return await _dbContext.SportsCourtImage
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>x.UniqueFileName==filename);
        }
    }
    public interface ISportsCourtImageRepository : IGenericRepository<SportsCourtImage>
    {
        //Task<List<SportsCourtImage>?> GetAllSportsCourtImageByIdCourt(int id);
        Task<SportsCourtImage?> GetByName(string filename);
    }
}
