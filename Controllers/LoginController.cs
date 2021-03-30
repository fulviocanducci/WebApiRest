using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiRest.Models;
using WebApiRest.Repositories;
using Canducci.GeneratePassword;
using WebApiRest.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly UserRepositoryAbstract _repository;
        private readonly BCrypt _crypt;
        private readonly ITokenService _token;
        public LoginController(UserRepositoryAbstract repository, BCrypt crypt, ITokenService token)
        {
            _crypt = crypt;
            _repository = repository;
            _token = token;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Login model)
        {
            await CreateUserDefault();
            User user = await _repository.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new
                {
                    Status = "Login not found"
                });
            }
            if (_crypt.Valid(model.Password, new BCryptValue(user.PasswordSalt, user.Password)))
            {
                var token = _token.GenerateToken(user);
                return Ok(new
                {
                    token
                });
            }
            return BadRequest(new
            {
                Status = "Login invalid"
            });
        }

        private async Task CreateUserDefault()
        {
            if (await _repository.CountAsync() == 0)
            {
                var pass = _crypt.Hash("123456@@");
                await _repository.AddAsync(new Models.User
                {
                    Name = "Fúlvio",
                    Email = "fulviocanducci@hotmail.com",
                    Password = pass.Hashed,
                    PasswordSalt = pass.Salt
                });
            }
            await Task.CompletedTask;
        }
    }
}