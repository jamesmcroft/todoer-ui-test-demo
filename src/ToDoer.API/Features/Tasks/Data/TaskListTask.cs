namespace ToDoer.API.Features.Tasks.Data
{
    using System;
    using ToDoer.API.Features.Tasks.Domain.UpdateTask;
    using ToDoer.API.Infrastructure.Data;

    public class TaskListTask : UserGeneratedEntityBase
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Important { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletedDate { get; set; }

        public TaskList List { get; set; }

        public Guid TaskListId { get; set; }

        public bool Update(UpdateTaskRequest request)
        {
            bool updated = false;

            if (Name != request.Name)
            {
                Name = request.Name;
                updated = true;
            }

            if (Note != request.Note)
            {
                Note = request.Note;
                updated = true;
            }

            if (DueDate != request.DueDate)
            {
                DueDate = request.DueDate;
                updated = true;
            }

            if (Important != request.Important)
            {
                Important = request.Important;
                updated = true;
            }

            if (Completed != request.Completed)
            {
                Completed = request.Completed;
                this.CompletedDate = this.Completed ? DateTime.UtcNow : default;
                updated = true;
            }

            return updated;
        }
    }
}