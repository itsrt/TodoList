using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Dtos
{
    public record CreateTodoItemRequest(
        [Required, MaxLength(200)] string Header,
        [MaxLength(200)] string Detail,
        DateOnly? CompletedBefore);


}
