using Canducci.GeneratePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.Models;
using WebApiRest.Repositories;
using WebApiRest.ViewModels;

namespace WebApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserRepositoryAbstract _repository;
        private readonly BCrypt _crypt;
        public UsersController(UserRepositoryAbstract repository, BCrypt crypt)
        {
            _repository = repository;
            _crypt = crypt;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var model = await _repository.GetAsync(id);
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound(new { Status = "Not Found", Id = id });
        }

        [HttpPost()]
        public async Task<ActionResult<User>> PostUser(UserCreate model)
        {
            if (ModelState.IsValid)
            {
                var pass = _crypt.Hash(model.Password);
                User data = new User
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = pass.Hashed,
                    PasswordSalt = pass.Salt
                };
                await _repository.AddAsync(data);
                return CreatedAtAction(nameof(GetUser), new { data.Id }, data);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> PutUser(int id, UserEdit model)
        {
            if (id != model.Id)
            {
                ModelState.AddModelError("User.Id", "Id != User.Id");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var data = await _repository.GetAsync(id);
                    if (data != null)
                    {
                        data.Email = model.Email;
                        data.Name = model.Name;
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            var pass = _crypt.Hash(model.Password);
                            data.Password = pass.Hashed;
                            data.PasswordSalt = pass.Salt;
                        }
                        if (await _repository.EditAsync(data))
                        {
                            return Ok(data);
                        } 
                        else
                        {
                            return StatusCode(304, data);
                        }
                    }
                    return NotFound(new { Status = "Not Found", Id = id });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var model = await _repository.GetAsync(id);
            if (model != null)
            {
                if (await _repository.DeleteAsync(model))
                {
                    return Ok(new { Status = "Successfully deleted" });
                }
            }
            return NotFound(new { Status = "Not Found", Id = id });
        }
    }

}