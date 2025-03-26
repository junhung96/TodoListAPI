using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;
using TodoListAPI.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IDbConnection _dbConnection;

    public TodoRepository(IDbConnection dbConnection)
    {
        this._dbConnection = dbConnection;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _dbConnection.QueryAsync<TodoItem>("SELECT * FROM TodoItems");
    }

    // Using inline SQL with Dapper for simplicity and performance.
    // For complex queries, stored procedures would be preferable.
    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<TodoItem>(
            "SELECT * FROM TodoItems WHERE Id = @Id", new { Id = id });
    }


    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        // SQL query that inserts and returns the complete record
        var sql = @"
        INSERT INTO TodoItems (Name, Description, DueDate, Status, Priority, Category)
        OUTPUT INSERTED.*
        VALUES (@Name, @Description, @DueDate, @Status, @Priority, @Category)";

        // Execute the query and return the complete created item
        var createdItem = await _dbConnection.QuerySingleAsync<TodoItem>(sql, todoItem);
        return createdItem;
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        var sql = @"
            UPDATE TodoItems 
            SET Name = @Name, 
                Description = @Description, 
                DueDate = @DueDate, 
                Status = @Status, 
                Priority = @Priority, 
                Category = @Category
            WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(sql, todoItem);
    }

    public async Task DeleteAsync(int id)
    {
        await _dbConnection.ExecuteAsync(
            "DELETE FROM TodoItems WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<TodoItem>> GetByFilterAsync(string? status, DateTime? dueDate)
    {
        var sql = "SELECT * FROM TodoItems WHERE 1=1";
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(status))
        {
            sql += " AND Status = @Status";
            parameters.Add("Status", status);
        }

        if (dueDate.HasValue)
        {
            sql += " AND CAST(DueDate AS DATE) = CAST(@DueDate AS DATE)";
            parameters.Add("DueDate", dueDate.Value);
        }

        return await _dbConnection.QueryAsync<TodoItem>(sql, parameters);
    }

    public async Task<IEnumerable<TodoItem>> GetSortedAsync(string sortBy, bool ascending = true)
    {
        // Validate sort column to prevent SQL injection
        var validColumns = new[] { "Name", "DueDate", "Priority", "Status" };
        var column = validColumns.Contains(sortBy) ? sortBy : "Id";

        var direction = ascending ? "ASC" : "DESC";
        var sql = $"SELECT * FROM TodoItems ORDER BY {column} {direction}";

        return await _dbConnection.QueryAsync<TodoItem>(sql);
    }
}