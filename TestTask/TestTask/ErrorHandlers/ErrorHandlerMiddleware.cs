using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using TestTask.Exceptions;

namespace TestTask.ErrorHandlers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    UsersNotFoundException => (int)HttpStatusCode.NotFound,
                    UserNotFoundException => (int)HttpStatusCode.NotFound,
                    RoleNotFoundException => (int)HttpStatusCode.NotFound,
                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,
                    ArgumentException e => (int)HttpStatusCode.BadRequest,
                    InvalidCredentialException e => (int)HttpStatusCode.Unauthorized,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
