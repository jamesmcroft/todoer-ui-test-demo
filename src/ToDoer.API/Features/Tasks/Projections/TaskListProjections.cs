namespace ToDoer.API.Features.Tasks.Projections
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Features.Tasks.ViewModels;

    public static class TaskListProjections
    {
        public static Expression<Func<TaskList, TaskListSummaryViewModel>> ToTaskListSummaryViewModel()
        {
            return list => new TaskListSummaryViewModel { Id = list.Id, Name = list.Name };
        }

        public static Expression<Func<TaskList, TaskListDetailViewModel>> ToTaskListDetailViewModel()
        {
            return list => new TaskListDetailViewModel
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.Tasks.Select(task => new TaskDetailViewModel
                {
                    Id = task.Id,
                    Name = task.Name,
                    Note = task.Note,
                    DueDate = task.DueDate,
                    Important = task.Important,
                    Completed = task.Completed,
                    CompletedDate = task.CompletedDate,
                    CreatedDate = task.CreatedDate
                })
            };
        }
    }
}