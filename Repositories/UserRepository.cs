using WebApiRest.Services;

namespace WebApiRest.Repositories
{
    public class UserRepository : UserRepositoryAbstract
    {
        public UserRepository(DatabaseService database) : base(database)
        {
        }
    }
}