namespace ToDoer.API.Features.Tasks.Data
{
    using System;
    using System.Collections.Generic;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Features.Tasks.Domain.AddTaskList;
    using ToDoer.API.Features.Tasks.Domain.UpdateTaskList;
    using ToDoer.API.Infrastructure.Data;

    public class TaskList : UserGeneratedEntityBase
    {
        public TaskList()
        {
        }

        public TaskList(AddTaskListRequest request)
        {
            Name = request.Name;
            AssignedToId = request.User.Id;
        }

        public string Name { get; set; }

        public Guid AssignedToId { get; set; }

        public User AssignedTo { get; set; }

        public ICollection<TaskListTask> Tasks { get; set; } = new List<TaskListTask>();

        public TaskListTask AddTask(string name)
        {
            var task = new TaskListTask { TaskListId = Id, Name = name };
            Tasks.Add(task);
            return task;
        }

        public bool Update(UpdateTaskListRequest request)
        {
            bool updated = false;

            if (Name != request.Name)
            {
                Name = request.Name;
                updated = true;
            }

            return updated;
        }
    }
}