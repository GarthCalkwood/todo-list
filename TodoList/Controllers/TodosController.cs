using Microsoft.AspNetCore.Mvc;

using TodoList.Application.Repositories;
using TodoList.Contracts.Requests;
using TodoList.API.Mapping;

namespace TodoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoRepository _todoRepo;

    public TodosController(ITodoRepository todoRepository)
    {
        _todoRepo = todoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todoRepo.GetAllAsync();
        var response = todos.MapToResponse();
        return Ok(response);
    }   

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var todo = await _todoRepo.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        var response = todo.MapToResponse();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoRequest request)
    {
        var todo = request.MapToTodo();
        await _todoRepo.CreateAsync(todo);
        var response = todo.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTodoRequest request)
    {
        var todo = request.MapToTodo(id);
        var updated = await _todoRepo.UpdateAsync(todo);

        if (!updated)
        {
            return NotFound();
        }

        var response = todo.MapToResponse();
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _todoRepo.DeleteAsync(id);

        if (!deleted)
        {   
            return NotFound();
        }

        return Ok();
    }
}
