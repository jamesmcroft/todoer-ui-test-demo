namespace ToDoer.API.Infrastructure.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Http;

    public class GenericHttpContextExceptionHandler<TException> : IHttpContextExceptionHandler<TException>
        where TException : HttpResponseException
    {
        public async Task HandleAsync(HttpContext context, TException exception)
        {
            var response = new ExceptionResponse<TException>(exception.GetType().Name, exception.Message, exception);
            await context.Response.WriteJsonAsync(exception.StatusCode, response);
        }
    }
}