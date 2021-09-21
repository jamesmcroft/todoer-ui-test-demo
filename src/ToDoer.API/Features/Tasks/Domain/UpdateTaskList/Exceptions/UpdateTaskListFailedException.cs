namespace ToDoer.API.Features.Tasks.Domain.UpdateTaskList.Exceptions
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class UpdateTaskListFailedException : HttpResponseException
    {
        public UpdateTaskListFailedException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}