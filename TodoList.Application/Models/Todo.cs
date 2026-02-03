namespace TodoList.Application.Models;

public class Todo
{
    public required Guid Id { get; init; }
    public required string Name { get; set; } = String.Empty;
    public required bool IsComplete { get; set; }
}
