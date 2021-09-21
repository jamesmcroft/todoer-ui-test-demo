namespace ToDoer.API.Features.Tasks.QueryFilters
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Infrastructure.Data;

    public static class TaskListFilters
    {
        public static IQueryable<TaskList> GetMyTaskLists(this AppDbContext dbContext, Guid userId)
        {
            return dbContext.TaskLists.Where(x => x.AssignedToId == userId);
        }

        public static IQueryable<TaskList> WithTasks(this IQueryable<TaskList> taskLists)
        {
            return taskLists.Include(x => x.Tasks);
        }

        public static IQueryable<TaskList> GetMyTaskList(this AppDbContext dbContext, Guid userId, Guid taskListId)
        {
            return dbContext.TaskLists.Where(x => x.AssignedToId == userId && x.Id == taskListId);
        }
    }
}