using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csbe.Todo.Api.Models;
using Csbe.Todo.Api.DataAccess;

namespace Csbe.Todo.Api.Controllers
{
    [Route("api/TodoItem")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoContext _todoContext;

        public TodoItemController(TodoContext context)
        {
            _todoContext = context;
        }

        //GET: api/todoitem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> ReadAllTodoItems()
        {
            return await _todoContext.TodoItems.ToListAsync();
        }

        //GET: api/todoitem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> ReadTodoItemById(long id)
        {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if(todoItem == null) return NotFound();
            return todoItem;
        }

        //POST: api/todoitem
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem item)
        {
            _todoContext.TodoItems.Add(item);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ReadTodoItemById), new { id = item.Id }, item);
        }

        //PUT: api/todoitem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodoItem(long id, TodoItem item)
        {
            if(id != item.Id) return BadRequest();

            _todoContext.Entry(item).State = EntityState.Modified;
            try
            {
                await _todoContext.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        //DELETE api/todoitem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if(todoItem == null) return NotFound();

            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
