using System.Linq;
using System.Threading.Tasks;
using WebApiRest.Models;
using WebApiRest.Repositories.Base;
using WebApiRest.Services;

namespace WebApiRest.Repositories
{
    public abstract class TodoRepositoryAbstract : Repository<Todo>, IRepository<Todo>
    {
        public TodoRepositoryAbstract(DatabaseService database) : base(database)
        {
        }
    }
}