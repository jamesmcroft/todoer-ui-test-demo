namespace ToDoer.API.Features.Tasks.Domain.AddTask.Exceptions
{
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class AddTaskFailedException : HttpResponseException
    {
        public AddTaskFailedException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}