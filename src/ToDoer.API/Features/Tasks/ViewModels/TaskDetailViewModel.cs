namespace ToDoer.API.Features.Tasks.ViewModels
{
    using System;

    public class TaskDetailViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Important { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}