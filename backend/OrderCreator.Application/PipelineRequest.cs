using Microsoft.AspNetCore.Builder;
using OrderCreator.Application.Common.Middlewares;

namespace OrderCreator.Application
{
    public static class PipelineRequest
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
