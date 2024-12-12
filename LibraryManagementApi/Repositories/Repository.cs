using LibraryManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(LibraryDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }
    }
}