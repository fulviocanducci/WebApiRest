using WebApiRest.Services;

namespace WebApiRest.Repositories
{
    public class TodoRepository : TodoRepositoryAbstract
    {
        public TodoRepository(DatabaseService database) : base(database)
        {
        }
    }
}