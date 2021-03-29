using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiRest.Models;
using WebApiRest.Repositories;

namespace WebApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class TodosController : ControllerBase
    {
        private readonly TodoRepositoryAbstract _repository;
        public TodosController(TodoRepositoryAbstract repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var model = await _repository.GetAsync(id);
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound(new {Status = "Not Found", Id = id});
        }

        [HttpPost()]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                var model = await _repository.AddAsync(todo);
                return CreatedAtAction(nameof(GetTodo), new {todo.Id}, todo);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Todo>> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                ModelState.AddModelError("Todo.Id", "Id != Todo.Id");
            } 
            else 
            {
                if (ModelState.IsValid)
                {
                    if (await _repository.EditAsync(todo))
                    {
                        return NoContent();
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var model = await _repository.GetAsync(id);
            if (model != null)
            {
                if (await _repository.DeleteAsync(model))
                {
                    return Ok(new {Status = "Successfully deleted"});
                }
            }
            return NotFound(new {Status = "Not Found", Id = id});
        }
    }
}