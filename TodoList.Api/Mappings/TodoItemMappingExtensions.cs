using TodoList.Api.Dtos;
using TodoList.Api.Models;

namespace TodoList.Api.Mappings
{
    public static class TodoItemMappingExtensions
    {
        public static TodoItemResponse ToResponse(this TodoItem item) =>
            new(
                item.Id, item.Header, item.Detail, item.CompletedBefore, item.CreatedOn
                );

        public static TodoItem ToDomain(this CreateTodoItemRequest request) =>
            new()
            {
                Header = request.Header,
                Detail = request.Detail,
                CompletedBefore = request.CompletedBefore
            };

    }
}
