using FastEndpoints;
using Shared.Results;

namespace Api.Configurations;

public static class FastEndpointsConfig
{
    public static void UseFastEndpointsWithCustomErrors(this WebApplication app)
    {
        app.UseFastEndpoints(c =>
        {
            c.Endpoints.RoutePrefix = "api";
            c.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
            {
                var errors = failures.Select(ex => new ApiError(ex.PropertyName, ex.ErrorMessage)).ToList();
                return ApiResponse<object>.Fail("Dữ liệu không hợp lệ", errors);
            };
        });
    }
}