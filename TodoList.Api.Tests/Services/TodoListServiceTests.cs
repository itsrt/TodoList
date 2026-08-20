using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Api.Models;
using TodoList.Api.Services;
using TodoList.Api.Storage;

namespace TodoList.Api.Tests.Services
{
    /// <summary>
    /// Test to verify TodoListService interacted with ITodoStorage correctly
    /// </summary>
    public class TodoListServiceTests
    {
        [Fact]
        public void Create_ShouldAddTodoItem()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);


            var todoItem = new TodoItem
            {
                Header = "Morning Breakfast",
                Detail = "Boiled Egg and Avocados",
                CompletedBefore = DateOnly.MaxValue
            };

            //Act                        
            var result = service.Add(todoItem);

            //Assert
            storageMock.Verify(storageCalled => storageCalled.Add(todoItem), Times.Once);

        }

        [Fact]
        public void Delete_ShouldDeleteTodoItem()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);
            var todoItemId = 1;

            //Act                        
            var result = service.Delete(todoItemId);

            //Assert
            storageMock.Verify(storageCalled => storageCalled.Delete(todoItemId), Times.Once);

        }

        [Fact]
        public void Delete_NonExistingItem_ShouldReturnFalse()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);
            var todoItemId = 2;

            storageMock
                .Setup(storageItem => storageItem.Delete(todoItemId))
                .Returns(false);

            //Act                        
            var result = service.Delete(todoItemId);

            //Assert
            Assert.False(result);

        }

        [Fact]
        public void GetById_ShouldReturnTodoItem()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);


            var todoItem = new TodoItem
            {
                Header = "Morning Breakfast",
                Detail = "Boiled Egg and Avocados",
                CompletedBefore = DateOnly.MaxValue
            };

            storageMock
                .Setup(storageCalled => storageCalled.GetById(1))
                .Returns(todoItem);

            //Act
            var result = service.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(todoItem, result);
        }

        [Fact]
        public void GetById_NonExistingTodoItem_ShouldReturnTodoItem()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);


            storageMock
                .Setup(storageCalled => storageCalled.GetById(2))
                .Returns((TodoItem?)null);

            //Act
            var result = service.GetById(2);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_ShouldReturnTodoItemList()
        {
            //Arrange
            var storageMock = new Mock<ITodoStorage>();
            var service = new TodoService(storageMock.Object);


            var todoItems = new List<TodoItem>
            {

                new()
                {
                Header = "Morning Breakfast",
                Detail = "Boiled Egg and Avocados",
                CompletedBefore = DateOnly.MaxValue
                },
                new()
                {
                Header = "Lunch",
                Detail = "Chicken Sandwich",
                CompletedBefore = DateOnly.MaxValue
                }

            };

            storageMock
                .Setup(storageCalled => storageCalled.GetAll())
                .Returns(todoItems);

            //Act
            var result = service.GetAll();

            //Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(todoItems, result);

        }




    }
}
