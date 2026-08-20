using TodoList.Api.Models;

namespace TodoList.Api.Storage
{
    public class InMemoryTodoStorage : ITodoStorage
    {
        private readonly List<TodoItem> _todos = new();

        private int _count = 0;

        public TodoItem Add(TodoItem todoItem)
        {
            todoItem.Id = ++_count;
            todoItem.CreatedOn = DateTime.UtcNow;
            _todos.Add(todoItem);
            return todoItem;
        }

        public bool Delete(int id)
        {
            var todo = GetById(id);

            if (todo == null)
            {
                return false;
            }

            _todos.Remove(todo);

            return true;

        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos;
        }

        public TodoItem? GetById(int id)
        {
            return _todos.FirstOrDefault(todoItem => todoItem.Id == id);
        }

    }
}
