namespace ToDoer.API.Features.Tasks.Data
{
    using System;
    using ToDoer.API.Infrastructure.Data;

    public class Task : UserGeneratedEntityBase
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Important { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletedDate { get; set; }

        public TaskList List { get; set; }

        public Guid TaskListId { get; set; }
    }
}