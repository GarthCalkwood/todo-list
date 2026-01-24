using TodoList.Application.Models;

namespace TodoList.Application.Repositories;

public interface ITodoRepository
{
    Task<Todo?> GetAsync(Guid id);
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<bool> CreateAsync(Todo todo);
    Task<bool> UpdateAsync(Todo todo);
    Task<bool> DeleteAsync(Guid id);
}
