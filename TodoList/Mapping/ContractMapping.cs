using TodoList.Contracts.Requests;
using TodoList.Contracts.Responses;
using TodoList.Application.Models;

namespace TodoList.API.Mapping;

public static class Mapper
{
    public static Todo MapToTodo(this CreateTodoRequest request)
    {
        return new Todo
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsComplete = request.IsComplete
        };
    }

    public static Todo MapToTodo(this UpdateTodoRequest request, Guid id)
    {
        return new Todo
        {
            Id = id,
            Name = request.Name,
            IsComplete = request.IsComplete
        };
    }

    public static TodoResponse MapToResponse(this Todo todo)
    {
        return new TodoResponse
        {
            Id = todo.Id,
            Name = todo.Name,
            IsComplete = todo.IsComplete
        };
    }

    public static TodosResponse MapToResponse(this IEnumerable<Todo> todos)
    {
        return new TodosResponse
        {
            Todos = todos.Select(MapToResponse)
        };
    }
}
