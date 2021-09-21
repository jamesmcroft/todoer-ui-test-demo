namespace ToDoer.API.Features.Tasks.Domain.GetTaskList
{
    using System;
    using ToDoer.API.Features.Tasks.ViewModels;
    using ToDoer.API.Infrastructure.Identity;

    public class GetTaskListRequest : AuthenticatedRequestBase<TaskListDetailViewModel>
    {
        public GetTaskListRequest(AuthenticatedUser user, Guid taskListId)
            : base(user)
        {
            TaskListId = taskListId;
        }

        public Guid TaskListId { get; set; }
    }
}