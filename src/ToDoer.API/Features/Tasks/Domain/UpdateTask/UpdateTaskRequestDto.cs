namespace ToDoer.API.Features.Tasks.Domain.UpdateTask
{
    using System;

    public class UpdateTaskRequestDto
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Important { get; set; }

        public bool Completed { get; set; }
    }
}