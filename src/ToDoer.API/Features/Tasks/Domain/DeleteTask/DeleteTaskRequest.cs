namespace ToDoer.API.Features.Tasks.Domain.DeleteTask
{
    using System;
    using ToDoer.API.Infrastructure.Identity;

    public class DeleteTaskRequest : AuthenticatedRequestBase
    {
        public DeleteTaskRequest(AuthenticatedUser user, Guid taskListId, Guid taskId)
            : base(user)
        {
            TaskListId = taskListId;
            TaskId = taskId;
        }

        public Guid TaskListId { get; set; }

        public Guid TaskId { get; set; }
    }
}