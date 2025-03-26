using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoItemsController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems(
            [FromQuery] string? status,
            [FromQuery] DateTime? dueDate,
            [FromQuery] string? sortBy,
            [FromQuery] bool? ascending)
        {
            try
            {
                if (!string.IsNullOrEmpty(status) || dueDate.HasValue)
                {
                    return Ok(await _service.GetFilteredTodosAsync(status, dueDate));
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    return Ok(await _service.GetSortedTodosAsync(sortBy, ascending ?? true));
                }

                return Ok(await _service.GetAllTodosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _service.GetTodoByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _service.UpdateTodoAsync(todoItem);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                var createdItem = await _service.CreateTodoAsync(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _service.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
