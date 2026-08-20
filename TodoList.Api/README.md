# TodoList API

## Overview

TodoList API is a RESTful ASP.NET Core Web API that provides VIEW ADD DELETE operations for managing Todo items.

The API is designed using a layered approach, separating HTTP concerns, business logic, and data storage. It is consumed by the Angular TodoList client application.

## Technology Stack

- **.NET 10** / ASP.NET Core Web API
- **C#**
- **REST API**
- **xUnit** for integration testing
- **In-memory storage** for Todo data
- **OpenAPI** for API documentation
- **Angular** client application

## API Endpoints

Base URL: 

```text
https://localhost:7256/api/todoList

```

| Method | Endpoint             | Description                  |
| ------ | -------------------- | ---------------------------- |
| GET    | `/api/todoList`      | Retrieve all Todo items      |
| GET    | `/api/todoList/{id}` | Retrieve a Todo item by ID   |
| POST   | `/api/todoList`      | Create a new Todo item       |
| PUT    | `/api/todoList/{id}` | Update an existing Todo item |
| DELETE | `/api/todoList/{id}` | Delete a Todo item           |

## API Testing

The `TodoList.Api.http` file contains sample HTTP requests for manually testing the TodoList API from Visual Studio.

The requests can be used to verify the API endpoints independently of the Angular client.

## Running the API

### Prerequisites

- .NET 10 SDK
- Visual Studio 2026 or another compatible .NET development environment

### Start the API

From the `TodoList.Api` project directory, run:

```bash
dotnet run
```

## Configuration

The API uses `appsettings.json` for application configuration.

### CORS

Allowed client origins are configured through the `Cors:AllowedOrigins` setting.

Example:

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200"
    ]
  }
}
```
## Testing

The solution includes automated tests covering both unit-level and integration-level behaviour.

### Unit Tests

The unit test project uses **xUnit** to test application components in isolation, including:

- Services
- Controllers

### Integration Tests

The integration test project also uses **xUnit** to test end to end feature, including Storage.

The integration tests covers two scenarios below.
#### Add Todo item and Display it
#### Add Todo item, Display it followed by Delete and Display

Run the tests from the solution directory:

```bash
dotnet test
```


## Architecture & Design

The API follows a layered architecture with clear separation of responsibilities.

### Controller Layer

Controllers are responsible for HTTP concerns, including:

- Receiving HTTP requests i.e Dtos - CreateTodoItemRequest.cs 
- Returning appropriate HTTP responses i.e. Dtos - TodoResponse.cs
- Converting DTOs to Domain model before delegating operations to the service layer - ie. Domain Model TodoItem.cs

Controllers do not contain business logic or data-access logic.

### Service Layer

It coordinates operations between the controllers and the storage abstraction
Future Scope - additional business logic can be added here

### Storage Layer

Data access is abstracted behind `ITodoStorage`.

The current implementation uses `InMemoryTodoStorage`, allowing the API to run without an external database while keeping the application independent of the storage implementation.

### Dependency Injection

Dependencies are registered using ASP.NET Core's built-in dependency injection container.

For example:

```csharp
builder.Services.AddSingleton<ITodoStorage, InMemoryTodoStorage>();
builder.Services.AddSingleton<ITodoService, TodoService>();
```

## Project Structure

### TodoList.Api
- `Controllers` — API endpoints
- `Dtos` — TodoItem Request and TodoItem Response Model 
- `Mappings` — Mapping functions to convert Dtos to Domain and vice versa
- `Models` — Domain Entity Representation for Todo item
- `Services` — application/business logic
- `Storage` — data storage abstraction and in-memory implementation
- `Program.cs` — application configuration and dependency injection
- `TodoList.Api.http` — sample requests for manually testing the API

### TodoList.Api.IntegrationTests
-  integration tests

### TodoList.Api.UnitTests
-  Controller tests
-  Unit tests
