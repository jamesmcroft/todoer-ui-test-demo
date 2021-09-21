namespace ToDoer.API.Features.Tasks.Exceptions.TaskNotFound
{
    using System;
    using System.Net;
    using ToDoer.API.Infrastructure.Exceptions;

    public class TaskNotFoundException : HttpResponseException
    {
        public TaskNotFoundException(Guid taskListId, Guid taskId, string message)
            : base(HttpStatusCode.NotFound, message)
        {
            TaskListId = taskListId;
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }

        public Guid TaskListId { get; set; }
    }
}