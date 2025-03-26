using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoItem> CreateTodoAsync(TodoItem todoItem)
        {
            // Business logic validation example
            if (todoItem.DueDate < DateTime.Today)
            {
                throw new InvalidOperationException("Due date cannot be in the past");
            }

            return await _repository.CreateAsync(todoItem);
        }

        public async Task UpdateTodoAsync(TodoItem todoItem)
        {
            // Business logic example
            var existingItem = await _repository.GetByIdAsync(todoItem.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Todo item not found");
            }

            if (existingItem.Status == "Completed" && todoItem.Status != "Completed")
            {
                throw new InvalidOperationException("Cannot change status from Completed");
            }

            await _repository.UpdateAsync(todoItem);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetFilteredTodosAsync(string? status, DateTime? dueDate)
        {
            return await _repository.GetByFilterAsync(status, dueDate);
        }

        public async Task<IEnumerable<TodoItem>> GetSortedTodosAsync(string sortBy, bool ascending = true)
        {
            return await _repository.GetSortedAsync(sortBy, ascending);
        }
    }
}
