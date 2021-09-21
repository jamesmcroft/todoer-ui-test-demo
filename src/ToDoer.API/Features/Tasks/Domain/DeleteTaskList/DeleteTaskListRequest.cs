namespace ToDoer.API.Features.Tasks.Domain.DeleteTaskList
{
    using System;
    using ToDoer.API.Infrastructure.Identity;

    public class DeleteTaskListRequest : AuthenticatedRequestBase
    {
        public DeleteTaskListRequest(AuthenticatedUser user, Guid taskListId)
            : base(user)
        {
            TaskListId = taskListId;
        }

        public Guid TaskListId { get; set; }
    }
}