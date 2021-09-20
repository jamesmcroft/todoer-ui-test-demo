namespace ToDoer.API.Features.Tasks.Data
{
    using System;
    using System.Collections.Generic;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Infrastructure.Data;

    public class TaskList : UserGeneratedEntityBase
    {
        public string Name { get; set; }

        public Guid AssignedToId { get; set; }

        public User AssignedTo { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}