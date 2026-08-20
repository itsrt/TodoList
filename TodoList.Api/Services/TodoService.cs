using TodoList.Api.Models;
using TodoList.Api.Storage;

namespace TodoList.Api.Services
{
    public class TodoService(ITodoStorage storage) : ITodoService
    {
        private readonly ITodoStorage _storage = storage;

        public TodoItem Add(TodoItem todoItem)
        {
            return _storage.Add(todoItem);
        }

        public bool Delete(int id)
        {
            return _storage.Delete(id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _storage.GetAll();
        }

        public TodoItem? GetById(int id)
        {
            return _storage.GetById(id);
        }

    }
}
