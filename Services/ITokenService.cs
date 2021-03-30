using WebApiRest.Models;

namespace WebApiRest.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}