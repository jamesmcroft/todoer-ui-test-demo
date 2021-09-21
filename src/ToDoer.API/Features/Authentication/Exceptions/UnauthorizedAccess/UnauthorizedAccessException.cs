namespace ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class UnauthorizedAccessException : HttpResponseException
    {
        public UnauthorizedAccessException(string message)
            : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}