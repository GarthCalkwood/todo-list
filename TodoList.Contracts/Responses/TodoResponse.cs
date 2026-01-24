namespace TodoList.Contracts.Responses;

public class TodoResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required bool IsComplete { get; init; }
}
