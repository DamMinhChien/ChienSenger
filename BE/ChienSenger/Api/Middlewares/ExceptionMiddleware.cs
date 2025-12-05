using Shared.Results;

namespace Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        // Nó k bắt dc phần này
        catch (FluentValidation.ValidationException e)
        {
            var errors = e.Errors.Select(ex => new ApiError(ex.PropertyName, ex.ErrorMessage)).ToList();
            var response = ApiResponse<object>.Fail("Validation Error", errors);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(response);
        }
        // Nó k bắt dc phần này
        catch (Exception e)
        {
            var status = e switch
            {
                InvalidOperationException => 400,
                UnauthorizedAccessException => 401,
                KeyNotFoundException => 404,
                _ => 500
            };
            
            Console.WriteLine(e);
            
            var response = ApiResponse<object>.Fail(e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}