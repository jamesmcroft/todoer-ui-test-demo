namespace ToDoer.API.Features.Tasks.Domain.DeleteTask.Exceptions
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class DeleteTaskFailedException : HttpResponseException
    {
        public DeleteTaskFailedException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}