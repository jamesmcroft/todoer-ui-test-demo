namespace ToDoer.API.Features.Tasks.Domain.UpdateTaskList
{
    using System;
    using ToDoer.API.Infrastructure.Identity;

    public class UpdateTaskListRequest : AuthenticatedRequestBase
    {
        public UpdateTaskListRequest(AuthenticatedUser user, Guid taskListId, UpdateTaskListRequestDto listRequest)
            : base(user)
        {
            TaskListId = taskListId;
            Name = listRequest.Name;
        }

        public Guid TaskListId { get; set; }

        public string Name { get; set; }
    }
}