using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoList.Api.Dtos;
using TodoList.Api.Models;

namespace TodoList.Api.IntegrationTests
{
    public class TodoListEndpointIntegrationTest : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;


        public TodoListEndpointIntegrationTest()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Fact]
        public async Task Create_TodoItem_And_Get_Created_TodoItem()
        {

            var createRequest = new CreateTodoItemRequest("Do Breakfast", "Boiled Eggs, Avocados", DateOnly.MaxValue);

            var createResponse = await _client.PostAsJsonAsync(
                    "/api/todoList", createRequest);


            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            Assert.NotNull(createResponse.Headers.Location);


            var created = await createResponse.Content.ReadFromJsonAsync<TodoItemResponse>();

            Assert.NotNull(created);
            Assert.True(created!.Id > 0);
            Assert.Equal("Do Breakfast", created.Header);

            var fetched = await _client.GetFromJsonAsync<TodoItemResponse>($"/api/todoList/{created.Id}");

            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched!.Id);
            Assert.Equal("Do Breakfast", fetched!.Header);

        }

        [Fact]
        public async Task Create_TodoItem_And_Delete_Created_TodoItem()
        {

            var createRequest = new CreateTodoItemRequest("Do Breakfast", "Boiled Eggs, Avocados", DateOnly.MaxValue);

            var createResponse = await _client.PostAsJsonAsync(
                    "/api/todoList", createRequest);


            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            Assert.NotNull(createResponse.Headers.Location);


            var created = await createResponse.Content.ReadFromJsonAsync<TodoItemResponse>();

            Assert.NotNull(created);
            Assert.True(created!.Id > 0);
            Assert.Equal("Do Breakfast", created.Header);

            var fetched = await _client.GetFromJsonAsync<TodoItemResponse>($"/api/todoList/{created.Id}");

            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched!.Id);
            Assert.Equal("Do Breakfast", fetched!.Header);

            var deleteTodoItemResponse = await _client.DeleteAsync($"/api/todoList/{created.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteTodoItemResponse.StatusCode);

            var getResponse = await _client.GetAsync($"/api/todoList/{created.Id}");


            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        }
    }
}
