using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(int id);
        Task<IEnumerable<TodoItem>> GetByFilterAsync(string? status, DateTime? dueDate);
        Task<IEnumerable<TodoItem>> GetSortedAsync(string sortBy, bool ascending = true);
    }
}
