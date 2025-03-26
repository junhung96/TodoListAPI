using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<TodoItem> CreateTodoAsync(TodoItem todoItem);
        Task UpdateTodoAsync(TodoItem todoItem);
        Task DeleteTodoAsync(int id);
        Task<IEnumerable<TodoItem>> GetFilteredTodosAsync(string? status, DateTime? dueDate);
        Task<IEnumerable<TodoItem>> GetSortedTodosAsync(string sortBy, bool ascending = true);
    }
}
