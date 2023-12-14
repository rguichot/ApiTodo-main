namespace MvcTodo.Models;

public class List
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<Todo> Todos { get; set; } = new();
}