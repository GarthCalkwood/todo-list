namespace TodoList.Contracts.Requests;

public class UpdateTodoRequest
{
    public required string Name { get; init; } = String.Empty;
    public required bool IsComplete { get; init; }
}
    