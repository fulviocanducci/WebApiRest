using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiRest.Repositories.Base
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> AddAsync(T model);
        Task<bool> EditAsync(T model);
        Task<bool> DeleteAsync(params object[] keys);
        Task<bool> DeleteAsync(T model);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(params object[] keys);
        Task<int> SaveAsync();
    }
}