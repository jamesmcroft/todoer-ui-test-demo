namespace ToDoer.API.Features.Tasks.Domain.DeleteTaskList.Exceptions
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class DeleteTaskListFailedException : HttpResponseException
    {
        public DeleteTaskListFailedException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}