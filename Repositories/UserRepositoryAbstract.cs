using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiRest.Models;
using WebApiRest.Repositories.Base;
using WebApiRest.Services;

namespace WebApiRest.Repositories
{
    public abstract class UserRepositoryAbstract : Repository<User>, IRepository<User>
    {
        public UserRepositoryAbstract(DatabaseService database) : base(database)
        {

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _model
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}