using Canducci.GeneratePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiRest.Models;
using WebApiRest.Repositories;
using WebApiRest.Services;

namespace WebApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _repository.GetUserByEmailAsync(User.Identity.Name));
        }

        [HttpPost]
        [AllowAnonymous]
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

        [NonAction]
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