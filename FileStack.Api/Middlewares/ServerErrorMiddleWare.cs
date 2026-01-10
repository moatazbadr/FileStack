
using FluentValidation;
using System.Text.Json;

namespace FileStack.Api.Middlewares
{
    public class ServerErrorMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            // to do add Not found Exception
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage)
                    );

                var response = new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    success = false,
                    message = "An unexpected error occurred.",
                    details = ex.Message
                };
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));

            }




        }
    }
}
