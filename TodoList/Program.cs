using Dapper;

using TodoList.Application.Database;
using TodoList.Application.Repositories;

var builder = WebApplication.CreateBuilder(args);

// This enables snake_case to PascalCase mapping for Dapper (e.g. is_complete will automatically map to IsComplete)
DefaultTypeMap.MatchNamesWithUnderscores = true;

// Load local secrets if available
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
builder.Services.AddSingleton<IDbConnectionFactory>(_ => 
    new NpgsqlConnectionFactory(config["Database:ConnectionString"]!));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
