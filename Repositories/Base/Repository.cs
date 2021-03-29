using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiRest.Services;

namespace WebApiRest.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T: class, new()
    {
        private readonly DatabaseService _database;
        private readonly DbSet<T> _model;
        public Repository(DatabaseService database)
        {
            if (database is null)
            {
                throw new System.ArgumentNullException(nameof(database));
            }
            _database = database;
            _model = database.Set<T>();
        }

        public async Task<T> AddAsync(T model)
        {
            await _model.AddAsync(model);
            await SaveAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(params object[] keys)
        {
            var model = await GetAsync(keys);
            if (model != null)
            {
                _model.Remove(model);
                return await SaveAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(T model)
        {
            _model.Remove(model);
            return await SaveAsync() > 0;
        }

        public async Task<bool> EditAsync(T model)
        {
            _model.Update(model);
            return await SaveAsync() > 0;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _model.ToListAsync();
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await _model.FindAsync(keys);
        }

        public async Task<int> SaveAsync()
        {
            return await _database.SaveChangesAsync();
        }
    }
}