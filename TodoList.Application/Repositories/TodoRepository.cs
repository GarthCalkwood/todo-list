using Dapper;

using TodoList.Application.Models;
using TodoList.Application.Database;

namespace TodoList.Application.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public TodoRepository(IDbConnectionFactory dbConn)
    {
        _dbConnectionFactory = dbConn;
    }

    public async Task<Todo?> GetAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var result = await connection.QuerySingleOrDefaultAsync<Todo>(new CommandDefinition("""
            select id, name, is_complete from tbTodo where id = @Id
            """, new { Id = id }));

        return result;
    }
    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var result = await connection.QueryAsync<Todo>(new CommandDefinition("""
            select id, name, is_complete from tbTodo
            """));

        return result;
    }

    public async Task<bool> CreateAsync(Todo todo)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into tbTodo (id, name, is_complete)
            values (@Id, @Name, @IsComplete)
            """, new { Id = todo.Id, Name = todo.Name, IsComplete = todo.IsComplete }));

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Todo todo)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var deleted = await connection.ExecuteAsync(new CommandDefinition("""
            delete from tbTodo where id = @Id
            """, new { Id = todo.Id }));

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into tbTodo (id, name, is_complete)
            values (@Id, @Name, @IsComplete)
            """, new { Id = todo.Id, Name = todo.Name, IsComplete = todo.IsComplete }));

        if (deleted == 0 || result == 0)
        {
            return false;
        }

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            delete from tbTodo where id = @Id
            """, new { Id = id }));

        return result > 0;
    }
}
