using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OtpRepository : GenericRepository<Otp>, IOtpRepository
    {
        public OtpRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<Otp?> GetByFilters(string email, string purpose)
        {
            return await _dbContext.Otp
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Email == email && o.Purpose == purpose && o.ExpiryDate > DateTime.UtcNow);
        }
        public async Task<List<Otp>> GetByEmailPurpose(string email, string purpose)
        {
           return await _dbContext.Otp
                .Where(o => o.Email == email && o.Purpose == purpose)
                   .AsNoTracking()
                .ToListAsync();
        }
    }
    public interface IOtpRepository : IGenericRepository<Otp>
    {
        Task<Otp?> GetByFilters(string email, string purpose);
        Task<List<Otp>> GetByEmailPurpose(    string email, string purpose);
    }
}
