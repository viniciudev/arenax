using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class NotificationsRepository : GenericRepository<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
        public async Task<List<Notifications>> GetAllByIdClient(int id)
        {
            return (await _dbContext.Set<Notifications>()
                 .Where(x => x.IdClient == id)
                 .Include(x => x.Client)
                 .AsNoTracking().ToListAsync());
        }
    }
    public interface INotificationsRepository : IGenericRepository<Notifications>
    {
        Task<List<Notifications>> GetAllByIdClient(int id);
    }
}
