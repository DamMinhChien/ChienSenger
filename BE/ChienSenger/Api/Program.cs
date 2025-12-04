using Api.Middlewares;
using FastEndpoints;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi();
}

app.UseStaticFiles();

// Catch global exceptions
app.UseExceptionMiddleware();

app.UseFastEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();