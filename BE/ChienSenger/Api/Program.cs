using Api.Middlewares;
using Application;
using FastEndpoints;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

//DI
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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