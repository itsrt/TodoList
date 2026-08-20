using TodoList.Api.Models;

namespace TodoList.Api.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Add(TodoItem item);
        bool Delete(int id);
    }
}
