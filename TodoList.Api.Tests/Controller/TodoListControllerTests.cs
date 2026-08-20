using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Reflection.PortableExecutable;
using TodoList.Api.Controllers;
using TodoList.Api.Dtos;
using TodoList.Api.Models;
using TodoList.Api.Services;
using TodoList.Api.Tests.Services;

namespace TodoList.Api.Tests.Controller
{
    public class TodoListControllerTests
    {
        [Fact]
        public void GetAll_ShouldReturn_Ok()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();

            var todoItems = new List<TodoItem>
            {

                new()
                {
                    Id= 1,
                    Header = "Morning Breakfast",
                    Detail = "Boiled Egg and Avocados",
                    CompletedBefore = DateOnly.MaxValue
                },
                new()
                {
                    Id = 2,
                    Header = "Lunch",
                    Detail = "Chicken Sandwich",
                    CompletedBefore = DateOnly.MaxValue
                }

            };

            List<TodoItemResponse> todoItemsResponse =
            [
                new(1, "Morning Breakfast", "Boiled Egg and Avocados", new DateOnly(2026, 8, 25), DateTime.Now),
                new(2, "Lunch", "Chicken Sandwich", new DateOnly(2026, 8, 25), DateTime.Now)
            ];


            serviceMock
                .Setup(service => service.GetAll())
                .Returns(todoItems);

            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.GetAll();


            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnTodoItems = Assert
                            .IsAssignableFrom<IEnumerable<TodoItemResponse>>(okResult.Value)
                            .ToList();

            Assert.Equal(2, returnTodoItems.Count());
            Assert.Equal(1, returnTodoItems[0].Id);

        }

        [Fact]
        public void GetById_TodoItemDoesNotExist_ShouldReturn_NotFound()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();



            serviceMock
                .Setup(service => service.GetById(2))
                .Returns((TodoItem?)null);


            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.GetById(2);


            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void GetById_TodoItemExist_ShouldReturn_OK()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();

            var todoItemRequest = new CreateTodoItemRequest(
                "Morning Breakfast",
                "Boiled Egg and Avocados",
                DateOnly.MaxValue
                );

            var createdTodoItem = new TodoItem
            {
                Id = 1,
                Header = "Morning Breakfast",
                Detail = "Boiled Egg and Avocados",
                CompletedBefore = DateOnly.MaxValue,
                CreatedOn = DateTime.Now
            };

            serviceMock
                .Setup(service => service.GetById(1))
                .Returns(createdTodoItem);


            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.GetById(1);


            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTodoItemResponse = Assert.IsType<TodoItemResponse>(okResult.Value);

            Assert.Equal("Morning Breakfast", returnTodoItemResponse.Header);
            Assert.Equal(1, returnTodoItemResponse.Id);

        }

        [Fact]
        public void Create_TodoItem_ShouldReturn_Created()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();

            var todoItemRequest = new CreateTodoItemRequest(
                "Morning Breakfast",
                "Boiled Egg and Avocados",
                DateOnly.MaxValue
                );

            var createdTodoItem = new TodoItem
            {
                Id = 1,
                Header = "Morning Breakfast",
                Detail = "Boiled Egg and Avocados",
                CompletedBefore = DateOnly.MaxValue,
                CreatedOn = DateTime.Now
            };

            serviceMock
                .Setup(service => service.Add(It.IsAny<TodoItem>()))
                .Returns(createdTodoItem);


            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.Create(todoItemRequest);


            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);


            Assert.Equal(nameof(TodoListController.GetById), createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues!["id"]);

            var response = Assert.IsType<TodoItemResponse>(createdResult.Value);

            Assert.Equal(1, response.Id);
            Assert.Equal("Morning Breakfast", response.Header);
            Assert.Equal("Boiled Egg and Avocados", response.Detail);

        }

        [Fact]
        public void Delete_Existing_TodoItem_ShouldReturn_NoContent()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();


            serviceMock
                .Setup(service => service.Delete(1))
                .Returns(true);


            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.Delete(1);


            //Assert
            var createdResult = Assert.IsType<NoContentResult>(result);


            serviceMock.Verify(
                service => service.Delete(1),
                Times.Once);

        }


        [Fact]
        public void Delete_NonExisting_TodoItem_ShouldReturn_NotFound()
        {
            //Arrange
            var serviceMock = new Mock<ITodoService>();


            serviceMock
                .Setup(service => service.Delete(2))
                .Returns(false);


            var controller = new TodoListController(serviceMock.Object);

            //Act
            var result = controller.Delete(2);


            //Assert
            Assert.IsType<NotFoundResult>(result);


            serviceMock.Verify(
                service => service.Delete(2),
                Times.Once);

        }

    }
}
