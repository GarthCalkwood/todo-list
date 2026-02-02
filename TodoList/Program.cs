using TodoList.Application.Database;
using TodoList.Application.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Load local secrets if available
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
builder.Services.AddSingleton<IDbConnectionFactory>(_ => 
    new NpgsqlConnectionFactory(config["Database:ConnectionString"]!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
