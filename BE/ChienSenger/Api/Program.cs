using Api.Configurations;
using Api.Middlewares;
using Api.Services;
using Application;
using Application.Interfaces.Common;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationWithJwtBearer(builder.Configuration);

//DI
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Exception middleware
app.UseExceptionMiddleware();

// HTTPS redirection
app.UseHttpsRedirection();

// Static files
app.UseStaticFiles();

// Swagger
if (app.Environment.IsDevelopment()) 
{
    app.UseSwaggerGen();
}

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// FastEndpoints
app.UseFastEndpointsWithCustomErrors();

app.Run();
