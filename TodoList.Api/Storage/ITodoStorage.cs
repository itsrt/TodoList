using TodoList.Api.Models;

namespace TodoList.Api.Storage
{
    public interface ITodoStorage
    {
        IEnumerable<TodoItem> GetAll();

        TodoItem? GetById(int id);

        TodoItem Add(TodoItem item);

        bool Delete(int id);

    }
}
