namespace TodoList.Api.Dtos
{
    public record TodoItemResponse(
        int Id,
        string Header,
        string? Detail,
        DateOnly? CompletedBy,
        DateTime CreatedOn
        );
}
