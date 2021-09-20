namespace ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess
{
    using System.Net;
    using System.Threading.Tasks;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Http;

    public class UnauthorizedAccessExceptionHandler : IHttpContextExceptionHandler<UnauthorizedAccessException>
    {
        public async Task HandleAsync(HttpContext context, UnauthorizedAccessException exception)
        {
            var response = new UnauthorizedAccessResponse(exception);
            await context.Response.WriteJsonAsync(HttpStatusCode.Unauthorized, response);
        }
    }
}