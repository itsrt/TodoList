using TodoList.Api.Services;
using TodoList.Api.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ITodoStorage, InMemoryTodoStorage>();
builder.Services.AddSingleton<ITodoService, TodoService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var allowedOrigins = builder.Configuration
    .GetSection("Cors")
    .GetSection("AllowedOrigins")
    .Get<string[]>() ?? [];

// Enable Angular Client Request
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientUI", policy =>
    {
        policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("ClientUI");


app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
