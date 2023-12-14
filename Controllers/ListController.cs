using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTodo.Models;

namespace ApiTodo.Controllers;

[ApiController]
[Route("api/list")]
public class ListController : ControllerBase
{
    private readonly TodoContext _context;

    public ListController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<List>>> GetLists()
    {
        // Get todos and related lists
        var lists = _context.Lists.Include(t => t.Todos);
        return await lists.ToListAsync();
    }

    // GET: api/todo/2
    [HttpGet("{id}")]
    public async Task<ActionResult<List>> GetList(int id)
    {
        // Find todo and related list
        // SingleAsync() throws an exception if no todo is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var list = await _context.Lists.Include(t => t.Todos).SingleOrDefaultAsync(t => t.Id == id);

        if (list == null)
            return NotFound();

        return list;
    }
}