using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csbe.Todo.Api.Models;
using Csbe.Todo.Api.DataAccess;

namespace Csbe.Todo.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _userContext;

        public UserController(TodoContext context)
        {
            _userContext = context;
        }

        //GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> ReadAllUsers()
        {
            return await _userContext.Users.ToListAsync();
        }

        //GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> ReadTodoItemById(long id)
        {
            var user = await _userContext.Users.FindAsync(id);
            if(user == null)
            return NotFound();

            return user;
        }

        //POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ReadTodoItemById), new { id = user.Id }, user);
        }

        //PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(long id, User user)
        {
            if(id != user.Id) return BadRequest();

            _userContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        //DELETE api/user/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> UserItem(long id)
        {
            var user = await _userContext.Users.FindAsync(id);
            if(user == null) return NotFound();

            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }
    }

}
