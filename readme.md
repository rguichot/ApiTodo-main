# Correction
Here the detailled correction, step-by-step

## Part 1
New API project
```
git init
dotnet new webapi -n ApiTodo
dotnet new gitignore
```

Add packages related to entity framework
```
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

Does-it work?
```
dotnet build
```

## Part 2
Create database
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Add a file SeedData.cs that contains a Init which is executed only if the DB is empty

## Part 3
Note: see your API documentation via swagger: https://localhost:<replace-by-your-port>/swagger/index.html

- Get all todos
```
curl -X 'GET' \
  'https://localhost:7044/api/todo' \
  -H 'accept: text/plain'
```

- Get a specific todo
```
curl -X 'GET' \
  'https://localhost:7044/api/todo/1' \
  -H 'accept: text/plain'
```

- Create a todo
```
curl -X 'POST' \
  'https://localhost:7044/api/todo' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "task": "Turn off the lights",
  "completed": false,
  "deadline": "2023-12-31T23:00:00.000Z"
}'
```

- Update a todo
```
curl -X 'PUT' \
  'https://localhost:7044/api/todo/4' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 4,
  "task": "Turn off the lights",
  "completed": true,
  "deadline": "2023-12-31T23:00:00"
}'
```

- Remove a todo
```
curl -X 'DELETE' \
  'https://localhost:7044/api/todo/4' \
  -H 'accept: */*'
```

## Part 4
- New class diagram with List + Todo

![Class diagram with List + Todo](/Resources/todo+list.png)

- Update the database
```
dotnet ef migrations add AddListToDb
dotnet ef database update
```

- Update the API response with the list attribute by using the key Include
```
var todos = _context.Todos.Include(t => t.List);
var todo = await _context.Todos.Include(t => t.List).SingleOrDefaultAsync(t => t.Id == id);
```

## Part 5
Add package for annotations
```
dotnet add package Swashbuckle.AspNetCore.Annotations
```

Adjust program.cs to include annotations on swagger
```
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
```

## Part 6
Prevent circular references when serializing objects to JSON
```
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
```

What are the impacts?
- The GET response of the API is now different, ToDo has reference to List
- The POST and PUT are no longer working, because the API expects a ListId -> This is a regression