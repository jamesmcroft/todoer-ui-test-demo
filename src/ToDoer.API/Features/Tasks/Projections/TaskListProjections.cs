namespace ToDoer.API.Features.Tasks.Projections
{
    using System;
    using System.Linq.Expressions;
    using Data;
    using ViewModels;

    public static class TaskListProjections
    {
        public static Expression<Func<TaskList, TaskListSummaryViewModel>> ToTaskListSummaryViewModel()
        {
            return list => new TaskListSummaryViewModel { Id = list.Id, Name = list.Name };
        }
    }
}