namespace ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound
{
    using System;
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class TaskListNotFoundException : HttpResponseException
    {
        public TaskListNotFoundException(Guid taskListId, string message)
            : base(HttpStatusCode.NotFound, message)
        {
            TaskListId = taskListId;
        }

        public Guid TaskListId { get; set; }
    }
}