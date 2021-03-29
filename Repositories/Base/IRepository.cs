using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiRest.Repositories.Base
{
    public interface IRepository<T> where T: class, new()
    {
        Task<T> AddAsync(T model);
        Task<bool> EditAsync(T model);
        Task<bool> DeleteAsync(params object[] keys);
        Task<bool> DeleteAsync(T model);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(params object[] keys);
        Task<int> SaveAsync();
    }
}