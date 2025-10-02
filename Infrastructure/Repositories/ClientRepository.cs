using Core;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DbContextClass dbContext) : base(dbContext)
        {

        }
      
        public async Task<ResponseClient?> GetClientsByPhone(string phone)
        {
            var resp = await (from c in  _dbContext.Client
                              .Include(x=>x.User)
                where c.Cellphone == phone
                select new ResponseClient
                {
                    Name = c.Name,
                    Cellphone = c.Cellphone,
                    Email = c.Email,
                    Id = c.Id,
                    EmailUser=c.User.Email,
                    NameUser=c.User.Name,
                    TypeUser=c.User.Type,
                    IdUser=c.User.Id
                })
                .AsNoTracking().FirstOrDefaultAsync();
            return resp;
        }
        public async Task<List<Client>?> GetAllClients()
        {
            return await _dbContext.Client.AsNoTracking().ToListAsync();
        }

    }
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<List<Client>?> GetAllClients();
        Task<ResponseClient?> GetClientsByPhone(string phone);
    }
}
