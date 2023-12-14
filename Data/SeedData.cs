using MvcTodo.Models;

public class SeedData
{
    public static void Init()
    {
        using var context = new TodoContext();

        // Look for existing content
        if (context.Todos.Any())
        {
            return;   // DB already filled
        }

        Todo todo1 = new()
        {
            Task = "Wash the dishes",
            Completed = true
        };
        Todo todo2 = new()
        {
            Task = "Clean the house",
            Deadline = DateTime.Now
        };
        Todo todo3 = new()
        {
            Task = "Do the laundry",
            Deadline = DateTime.Now.AddDays(10)
        };
        context.Todos.AddRange(todo1, todo2, todo3);

        // Commit changes into DB
        context.SaveChanges();
    }

    public static void CompleteSeedDataWithLists()
    {
        using var context = new TodoContext();

        // Look for existing content
        if (context.Lists.Any())
        {
            return;   // DB already filled
        }

        var todo1 = context.Todos.ToList()[0];
        var todo2 = context.Todos.ToList()[1];

        List list1 = new() { Name = "Chores" };
        list1.Todos.Add(todo1);
        list1.Todos.Add(todo2);
        todo1.List = list1;
        todo2.List = list1;
        List list2 = new() { Name = "Holidays" };

        context.Lists.AddRange(
            list1,
            list2
        );

        // Commit changes into DB
        context.SaveChanges();
    }
}