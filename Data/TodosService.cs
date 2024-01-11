using Cafe.Components.Pages;
using Cafe.Data;
using System.Text.Json;

namespace Cafe.Data;

public static class TodosService
{
    private static void SaveAll(Guid userId, List<TodoItem> todos)
    {
        string appDataDirectoryPath = Util.GetAppDirectoryPath();
        string todosFilePath = Util.GetTodosFilePath(userId);

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(todos);
        File.WriteAllText(todosFilePath, json);
    }

    public static List<TodoItem> GetAll(Guid userId)
    {
        string todosFilePath = Util.GetTodosFilePath(userId);
        if (!File.Exists(todosFilePath))
        {
            return new List<TodoItem>();
        }

        var json = File.ReadAllText(todosFilePath);

        return JsonSerializer.Deserialize<List<TodoItem>>(json);
    }
    public static List<TodoItem> GetAll()
    {
        string appUsersFilePath = Util.GetAppTodosFilePath();
        if (!File.Exists(appUsersFilePath))
        {
            return new List<TodoItem>();
        }

        var json = File.ReadAllText(appUsersFilePath);

        return JsonSerializer.Deserialize<List<TodoItem>>(json);
    }


    public static List<TodoItem> Create(Guid userId, string taskName, int Price, DateTime dueDate)
    {
        if (dueDate < DateTime.Today)
        {
            throw new Exception("Due date must be in the future.");
        }

        List<TodoItem> todos = GetAll(userId);
        todos.Add(new TodoItem
        {
            Name = taskName,
            Price  = Price,
            DueDate = dueDate,
            CreatedBy = userId
        });
        SaveAll(userId, todos);
        return todos;
    }

    public static List<TodoItem> Delete(Guid userId, Guid id)
    {
        List<TodoItem> todos = GetAll(userId);
        TodoItem todo = todos.FirstOrDefault(x => x.Id == id);

        if (todo == null)
        {
            throw new Exception("Todo not found.");
        }

        todos.Remove(todo);
        SaveAll(userId, todos);
        return todos;
    }

    public static void DeleteByUserId(Guid userId)
    {
        string todosFilePath = Util.GetTodosFilePath(userId);
        if (File.Exists(todosFilePath))
        {
            File.Delete(todosFilePath);
        }
    }

    public static List<TodoItem> Update(Guid userId, Guid id, string taskName, int Price, DateTime dueDate, bool isDone)
    {
        List<TodoItem> todos = GetAll(userId);
        TodoItem todoToUpdate = todos.FirstOrDefault(x => x.Id == id);

        if (todoToUpdate == null)
        {
            throw new Exception("Todo not found.");
        }

        todoToUpdate.Name = taskName;
        todoToUpdate.IsDone = isDone;
        todoToUpdate.DueDate = dueDate;
        todoToUpdate.Price = Price;
        SaveAll(userId, todos);
        return todos;
    }
}
