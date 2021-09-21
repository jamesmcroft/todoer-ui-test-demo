namespace ToDoer.API.Features.Tasks.Domain.AddTask
{
    using System;
    using ToDoer.API.Infrastructure.Identity;

    public class AddTaskRequest : AuthenticatedRequestBase
    {
        public AddTaskRequest(AuthenticatedUser user, Guid taskListId, AddTaskRequestDto request)
            : base(user)
        {
            TaskListId = taskListId;
            Name = request.Name;
        }

        public Guid TaskListId { get; set; }

        public string Name { get; set; }
    }
}