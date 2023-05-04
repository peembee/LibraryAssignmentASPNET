using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using LibraryAssignment.Data;
using LibraryAssignment.Repository.IRepository;

namespace LibraryAssignment.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryContext _libraryDatabase;
        internal DbSet<T> dbset;
        public Repository(LibraryContext libraryDatabase)
        {
            _libraryDatabase = libraryDatabase;
            dbset = _libraryDatabase.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> temp = dbset;
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> temp = dbset;
            if (!tracked == true)
            {
                temp = temp.AsNoTracking();
            }
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _libraryDatabase.SaveChangesAsync();
        }
    }
}
