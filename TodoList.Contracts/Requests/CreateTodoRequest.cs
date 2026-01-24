namespace TodoList.Contracts.Requests;

public class CreateTodoRequest
{
    public required string Name { get; init; } = String.Empty;
    public required bool IsComplete { get; init; }
}
