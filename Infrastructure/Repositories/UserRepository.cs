using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContextClass dbContext) : base(dbContext)
        {
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Set<User>()
                .Where(x => x.Email == email)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserById(int userId)
        {
            return await _dbContext.Set<User>()
             .Where(x => x.Id == userId)
             .Include(x=>x.Client)
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int userId);
    }
}
