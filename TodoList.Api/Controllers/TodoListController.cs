using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Dtos;
using TodoList.Api.Mappings;
using TodoList.Api.Models;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoListController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_todoService.GetAll().Select(eachtodoItem => eachtodoItem.ToResponse()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todoItem = _todoService.GetById(id);

            if (todoItem is null)
            {
                return NotFound();
            }

            return Ok(todoItem.ToResponse());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TodoItemResponse> Create([FromBody] CreateTodoItemRequest todoItemRequest)
        {
            var createdTodoItem = _todoService.Add(todoItemRequest.ToDomain());

            return CreatedAtAction(nameof(GetById), new { id = createdTodoItem.Id }, createdTodoItem.ToResponse());
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleted = _todoService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
