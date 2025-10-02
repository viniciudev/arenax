using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   

    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<(IEnumerable<T> Items, int TotalCount)> GetAll(int pageNumber, int pageSize); // Atualizado para retornar dados paginados
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        public GenericRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
