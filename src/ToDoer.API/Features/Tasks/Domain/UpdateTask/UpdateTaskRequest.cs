namespace ToDoer.API.Features.Tasks.Domain.UpdateTask
{
    using System;
    using ToDoer.API.Infrastructure.Identity;

    public class UpdateTaskRequest : AuthenticatedRequestBase
    {
        public UpdateTaskRequest(AuthenticatedUser user, Guid taskListId, Guid taskId, UpdateTaskRequestDto request)
            : base(user)
        {
            TaskListId = taskListId;
            TaskId = taskId;
            Name = request.Name;
            Note = request.Note;
            DueDate = request.DueDate;
            Important = request.Important;
            Completed = request.Completed;
        }

        public Guid TaskId { get; set; }

        public Guid TaskListId { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Important { get; set; }

        public bool Completed { get; set; }
    }
}