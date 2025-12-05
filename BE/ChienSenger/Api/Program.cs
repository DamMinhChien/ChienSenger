using Api.Configurations;
using Api.Middlewares;
using Application;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.UseStaticFiles();

// Catch global exceptions

app.UseExceptionMiddleware();

app.UseAuthentication();

app.UseAuthorization();

// app.UseDeveloperExceptionPage();

app.UseFastEndpointsWithCustomErrors();

app.UseHttpsRedirection();

app.Run();