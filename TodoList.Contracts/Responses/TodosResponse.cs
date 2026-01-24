namespace TodoList.Contracts.Responses;

public class TodosResponse
{
    public required IEnumerable<TodoResponse> Todos { get; init; } = Enumerable.Empty<TodoResponse>();
}
