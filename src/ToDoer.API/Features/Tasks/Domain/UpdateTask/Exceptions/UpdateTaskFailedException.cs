namespace ToDoer.API.Features.Tasks.Domain.UpdateTask.Exceptions
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class UpdateTaskFailedException : HttpResponseException
    {
        public UpdateTaskFailedException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}