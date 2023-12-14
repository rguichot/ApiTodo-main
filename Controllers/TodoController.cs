using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTodo.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiTodo.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/todo
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all todos",
        Description = "Get all todos and related lists")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Todos found", typeof(IEnumerable<Todo>))]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        // Get todos and related lists
        var todos = _context.Todos.Include(t => t.List);
        return await todos.ToListAsync();
    }

    // GET: api/todo/2
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a todo by id",
        Description = "Get a todo and related list by id")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Todo found", typeof(Todo))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Todo not found")]
    public async Task<ActionResult<Todo>> GetTodo([SwaggerParameter("The todo id", Required = true)] int id)
    {
        // Find todo and related list
        // SingleAsync() throws an exception if no todo is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var todo = await _context.Todos.Include(t => t.List).SingleOrDefaultAsync(t => t.Id == id);

        if (todo == null)
            return NotFound();

        return todo;
    }

    // POST: api/todo
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new todo",
        Description = "Create a new todo and related list")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "Todo created", typeof(Todo))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid todo")]
    public async Task<ActionResult<Todo>> PostTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    // PUT: api/todo/2
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a todo",
        Description = "Update a todo and related list")
    ]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Todo updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid todo")]
    public async Task<IActionResult> PutTodo([SwaggerParameter("The todo id", Required = true)] int id, Todo todo)
    {
        if (id != todo.Id)
            return BadRequest();

        _context.Entry(todo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Todos.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/todo/2
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a todo",
        Description = "Delete a todo and related list")
    ]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Todo deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Todo not found")]
    public async Task<IActionResult> DeleteTodoItem([SwaggerParameter("The todo id", Required = true)] int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
            return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
